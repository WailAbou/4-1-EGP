using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("PlayerManager References")]
    public Transform PlayersHolder;
    public GameObject PlayerPrefab;

    [Header("PlayerManager Settings")]
    public int PlayerAmount = 3;

    public Action<Transform, Vector2Int> OnTurnStart;
    public Action<Transform, Transform> OnMoveStart;
    public Action<Transform> OnMoveEnd;
    public Action<PlayerLogic[]> OnPlayersSpawned;

    private PlayerLogic[] _players;
    private PlayerLogic _currentPlayer;
    private List<Color> _colors = new List<Color>();
    private float _offset = 0.3f;
    private int _playerIndex;

    public override void Setup()
    {
        _players = new PlayerLogic[PlayerAmount];
        _colors.Fill(PlayerAmount);
        _generatorManager.OnAnimationsDone += (startGridCell) => StartCoroutine(SpawnPlayers(startGridCell));
    }

    private IEnumerator SpawnPlayers(GridCell startGridCell)
    {
        for (int i = 0; i < _players.Length; i++)
        {
            var startPos = startGridCell.gameObject.transform.position;
            var position = new Vector3(startPos.x + _offset * i, startPos.y, startPos.z);

            var playerObject = Instantiate(PlayerPrefab, PlayersHolder);
            var player = playerObject.GetComponent<PlayerLogic>();

            player.transform.position = position;
            player.Sail.material.color = _colors.PickRandom(true);
            _players[i] = player;

            yield return new WaitForSecondsRealtime(Constants.PLAYER_SPAWN_DURATION);
        }

        NextTurn();
        OnPlayersSpawned?.Invoke(_players);
    }

    public void NextTurn()
    {
        _currentPlayer = _players[_playerIndex];
        _uiManager.StartToastr($"Speler {_playerIndex + 1} beurt!");
        _playerIndex = (_playerIndex + 1) % PlayerAmount;
        Vector2Int gridPosition = ClosestGridCell(_currentPlayer.transform);
        OnTurnStart?.Invoke(_currentPlayer.transform, gridPosition);
    }

    private Vector2Int ClosestGridCell(Transform player)
    {
        int x = Mathf.RoundToInt((player.position.x + 1.35f) * 3);
        int y = Mathf.RoundToInt((player.position.z + 1.35f) * 3);
        return new Vector2Int(x, y);
    }

    public void TakeTurn(Transform target)
    {
        var player = _currentPlayer.transform;
        StartCoroutine(TakeTurnRoutine(player, target));
    }

    private IEnumerator TakeTurnRoutine(Transform player, Transform target)
    {
        OnMoveStart?.Invoke(player, target);
        yield return new WaitForSecondsRealtime(Constants.PLAYER_MOVE_DURATION + 1.0f);
        NextTurn();
        OnMoveEnd?.Invoke(_currentPlayer.transform);
    }
}
