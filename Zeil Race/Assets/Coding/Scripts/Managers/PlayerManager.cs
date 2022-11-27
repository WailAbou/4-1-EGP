using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("PlayerManager References")]
    public Transform PlayersHolder;
    public GameObject PlayerPrefab;

    public Action<Transform, Vector2Int> OnTurnStart;
    public Action<Transform, Transform> OnMoveStart;
    public Action<Transform> OnMoveEnd;
    public Action<List<PlayerLogic>> OnPlayersSpawned;

    private List<Color> _colors = new List<Color>();
    private List<PlayerLogic> _players = new List<PlayerLogic>();
    private PlayerLogic _currentPlayer;
    private int _playerIndex;
    public int _playerAmount = 3;

    public override void Setup()
    {
        _colors.FillRandom(_playerAmount);
        _generatorManager.OnAnimationsDone += (startGridCell, level) => StartCoroutine(SpawnPlayers(startGridCell, level)); ;
    }

    private IEnumerator SpawnPlayers(CellLogic startCell, Level level)
    {
        for (int i = 0; i < _playerAmount; i++)
        {
            var startCoordinates = level.StartPositions[i];
            var startPosition = _cellManager.GetPosition(startCoordinates);
            _uiManager.StartToastr($"Plaats speler X op de coordinaten: ({startCoordinates.x}, {startCoordinates.y})");
            yield return SpawnPlayer(startPosition, i);
        }

        NextTurn();
        OnPlayersSpawned?.Invoke(_players);
    }

    private IEnumerator SpawnPlayer(Vector3 startPositon, int i)
    {
        var position = new Vector3(startPositon.x, startPositon.y, startPositon.z);
        var player = Instantiate(PlayerPrefab, PlayersHolder).GetComponent<PlayerLogic>();

        player.transform.position = position;
        player.Sail.material.color = _colors.PickRandom(true);
        _players.Add(player);

        yield return new WaitForSecondsRealtime(Animations.PLAYER_SPAWN_DURATION);
    }

    public void NextTurn()
    {
        _uiManager.StartToastr($"Speler {_playerIndex + 1} beurt!");

        _currentPlayer = _players[_playerIndex];
        _playerIndex = (_playerIndex + 1) % _playerAmount;
        Vector2Int cellCoordinates = _cellManager.GetCoordinates(_currentPlayer.transform.position);

        OnTurnStart?.Invoke(_currentPlayer.transform, cellCoordinates);
    }

    public void TakeTurn(Transform target)
    {
        StartCoroutine(TakeTurnRoutine(target));
    }

    private IEnumerator TakeTurnRoutine(Transform target)
    {
        OnMoveStart?.Invoke(_currentPlayer.transform, target);
        yield return new WaitForSecondsRealtime(Animations.PLAYER_MOVE_DURATION + 1.0f);
        NextTurn();
        OnMoveEnd?.Invoke(_currentPlayer.transform);
    }
}
