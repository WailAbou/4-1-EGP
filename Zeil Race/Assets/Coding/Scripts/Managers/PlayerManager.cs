using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("PlayerManager References")]
    public Transform PlayersHolder;
    public GameObject PlayerPrefab;

    public Action<Transform, Vector2Int> OnTurnStart;
    public Action<Transform, Transform> OnMoveStart;
    public Action<Transform> OnMoveEnd;
    public Action<PlayerLogic[]> OnPlayersSpawned;

    private Action<CellLogic> _checkPosition;
    private List<Color> _colors = new List<Color>();
    public PlayerLogic[] _players;
    private PlayerLogic _currentPlayer;
    private Vector2Int _coordinates;
    private Level _level;
    private int _playerAmount = 3;
    private int _playerIndex;
    private int _spawnedPlayers;

    public override void Setup()
    {
        _checkPosition = cell => StartCoroutine(CheckPosition(cell));
        _players = new PlayerLogic[_playerAmount];
        _colors.FillRandom(_playerAmount);

        _generatorManager.OnAnimationsDone += SetupPlayer;
        _cellManager.OnSelectCell += _checkPosition;
    }

    private void SetupPlayer(CellLogic startCell, Level level)
    {
        _level = level;
        _coordinates = _level.StartCoordinates[_spawnedPlayers];

        _uiManager.StartToastr($"Plaats speler {_playerIndex + 1} op de coordinaten: ({_coordinates.x}, {_coordinates.y})");
        _diceManager.EndRollDices(0);

        _playerIndex = Math.Max(_spawnedPlayers, (_playerIndex + 1) % _playerAmount);
    }

    private IEnumerator CheckPosition(CellLogic cell)
    {
        if (cell.Coordinates == _coordinates)
        {
            var startPosition = _cellManager.GetPosition(_coordinates);
            yield return SpawnPlayer(startPosition);
        }

        if (_players.Any(p => p == null))
        {
            yield return new WaitForEndOfFrame();
            SetupPlayer(null, _level);
        }
        else
        {
            _playerIndex = 0;
            _cellManager.OnSelectCell -= _checkPosition;

            NextTurn();
            OnPlayersSpawned?.Invoke(_players);
        }
    }

    private IEnumerator SpawnPlayer(Vector3 startPositon)
    {
        var position = new Vector3(startPositon.x, startPositon.y, startPositon.z);
        var player = Instantiate(PlayerPrefab, PlayersHolder).GetComponent<PlayerLogic>();

        player.transform.position = position;
        player.Sail.material.color = _colors.PickRandom(true);
        _players[_spawnedPlayers++] = player;

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
