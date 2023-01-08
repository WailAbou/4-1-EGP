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
    public PlayerLogic CurrentPlayer;

    private Action<CellLogic> _setupPlayerAction;
    private List<Color> _colors = new List<Color>();
    public PlayerLogic[] _players;
    private Vector2Int _coordinates;
    private Level _level;
    private int _playerAmount = 3;
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
        CurrentPlayer.SetName(name);
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
        CurrentPlayer = player;

        player.transform.position = position;
        player.Sail.material.color = _colors.PickRandom(true);
        _players[_spawnedPlayers++] = player;

        yield return new WaitForSecondsRealtime(Animations.PLAYER_SPAWN_DURATION);
    }

    public void NextTurn()
    {
        CurrentPlayer = _players[_playerIndex];
        _uiManager.StartToastr($"{CurrentPlayer.Name}'s beurt!", "Spel begonnen!");

        _playerIndex = (_playerIndex + 1) % _playerAmount;
        Vector2Int cellCoordinates = _cellManager.GetCoordinates(CurrentPlayer.transform.position);

        OnTurnStart?.Invoke(CurrentPlayer.transform, cellCoordinates);
    }

    public void TakeTurn(Transform target)
    {
        StartCoroutine(TakeTurnRoutine(target));
    }

    private IEnumerator TakeTurnRoutine(Transform target)
    {
        OnMoveStart?.Invoke(CurrentPlayer.transform, target);
        yield return new WaitForSecondsRealtime(Animations.PLAYER_MOVE_DURATION + 1.0f);
        NextTurn();
        OnMoveEnd?.Invoke(CurrentPlayer.transform);
    }

    public bool HasPlayer(Vector2Int coordinates)
    {
        if (_spawnedPlayers != _playerAmount) return false;
        bool hasPlayer = _players.Where(p => GetCoordinates(p.transform) == coordinates).Any();
        return hasPlayer;
    }

    private Vector2Int GetCoordinates(Transform target)
    {
        float offset = 1.35f;
        float gridSize = 0.3f;

        int x = Mathf.CeilToInt((target.position.x + offset) / gridSize);
        int y = Mathf.CeilToInt((target.position.z + offset) / gridSize);
        
        return new Vector2Int(x, y);
    }
}
