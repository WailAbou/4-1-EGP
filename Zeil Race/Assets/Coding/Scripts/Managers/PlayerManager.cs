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

    /// <summary>
    /// Spawns the players sets the colors and stores them, then going to the first turn.
    /// </summary>
    /// <param name="startGridCell">The origin point of the board, aka the starting gridcell.</param>
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

            yield return new WaitForSecondsRealtime(Animations.PLAYER_SPAWN_DURATION);
        }

        NextTurn();
        OnPlayersSpawned?.Invoke(_players);
    }

    /// <summary>
    /// Starting the next turn.
    /// </summary>
    public void NextTurn()
    {
        _currentPlayer = _players[_playerIndex];
        _uiManager.StartToastr($"Speler {_playerIndex + 1} beurt!");
        _playerIndex = (_playerIndex + 1) % PlayerAmount;
        OnTurnStart?.Invoke(_currentPlayer.transform, ClosestGridCell());
    }

    /// <summary>
    /// Getting the closest gridcell to the player.
    /// </summary>
    /// <returns>The closest grid cell.</returns>
    private Vector2Int ClosestGridCell()
    {
        int x = Mathf.RoundToInt((_currentPlayer.transform.position.x + 1.35f) * 3);
        int y = Mathf.RoundToInt((_currentPlayer.transform.position.z + 1.35f) * 3);
        return new Vector2Int(x, y);
    }

    public void TakeTurn(Transform target)
    {
        StartCoroutine(TakeTurnRoutine(target));
    }

    /// <summary>
    /// Starts the player movement, waits until the animation is done, goes on to the next turn and ends the turn.
    /// </summary>
    /// <param name="target">The target the player wants to move to.</param>
    private IEnumerator TakeTurnRoutine(Transform target)
    {
        OnMoveStart?.Invoke(_currentPlayer.transform, target);
        yield return new WaitForSecondsRealtime(Animations.PLAYER_MOVE_DURATION + 1.0f);
        NextTurn();
        OnMoveEnd?.Invoke(_currentPlayer.transform);
    }
}
