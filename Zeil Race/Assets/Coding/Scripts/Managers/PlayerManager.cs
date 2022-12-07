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

    private Action<CellLogic> _setupPlayerAction;
    private List<Color> _colors = new List<Color>();
    public PlayerLogic[] _players;
    private PlayerLogic _currentPlayer;
    private Vector2Int _coordinates;
    private Level _level;
    private int _playerAmount = 1;
    private int _playerIndex;
    private int _spawnedPlayers;

    public override void Setup()
    {
        _setupPlayerAction = cell => StartCoroutine(SetupPlayer(cell));
        _players = new PlayerLogic[_playerAmount];
        _colors.FillRandom(_playerAmount);

        _cellManager.OnSelectCell += _setupPlayerAction;
        _uiManager.OnEndName += InitPlayer;
        _generatorManager.OnAnimationsDone += SetupTurn;
    }

    private IEnumerator SetupPlayer(CellLogic cell)
    {
        bool correctCoordinates = (cell.Coordinates == _coordinates);
        
        if (correctCoordinates)
        {
            var startPosition = _cellManager.GetPosition(_coordinates);
            yield return SpawnPlayer(startPosition);
            _uiManager.StartName();
        } 
        else
        {
            yield return new WaitForEndOfFrame();
            SetupTurn(null, _level);
        }
    }

    private void InitPlayer(string name)
    {
        _currentPlayer.SetName(name);
        bool allPlayersSpawned = !_players.Any(p => p == null);

        if (allPlayersSpawned)
        {
            _playerIndex = 0;
            _cellManager.OnSelectCell -= _setupPlayerAction;
            _uiManager.OnEndName -= InitPlayer;

            NextTurn();
            OnPlayersSpawned?.Invoke(_players);
        }
        else
        {
            SetupTurn(null, _level);
        }
    }

    private void SetupTurn(CellLogic startCell, Level level)
    {
        _level = level;
        _coordinates = _level.StartCoordinates[_spawnedPlayers];

        _uiManager.StartToastr($"Plaats de speler {_playerIndex + 1} op ", $"coordinaten: ({_coordinates.x}, {_coordinates.y})");
        _diceManager.EndRollDices(0);
        
        _playerIndex = Math.Max(_spawnedPlayers, (_playerIndex + 1) % _playerAmount);
    }

    private IEnumerator SpawnPlayer(Vector3 startPositon)
    {
        var position = new Vector3(startPositon.x, startPositon.y, startPositon.z);
        var player = Instantiate(PlayerPrefab, PlayersHolder).GetComponent<PlayerLogic>();
        _currentPlayer = player;

        player.transform.position = position;
        player.Sail.material.color = _colors.PickRandom(true);
        _players[_spawnedPlayers++] = player;

        yield return new WaitForSecondsRealtime(Animations.PLAYER_SPAWN_DURATION);
    }

    public void NextTurn()
    {
        _uiManager.StartToastr($"Speler {_playerIndex + 1} beurt!", "Spel begonnen!");

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
