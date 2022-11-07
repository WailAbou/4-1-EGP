using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("GameManager References")]
    public Transform PlayersHolder;
    public GameObject PlayerPrefab;

    [Header("PlayerManager Settings")]
    public int PlayerAmount = 3;

    public Action<Transform> OnTurnStart;
    public Action<Transform, Vector3> OnMoveStart;
    public Action<Transform> OnMoveEnd;
    public Action<PlayerMechanic[]> OnPlayersSpawned;

    private BoardManager _boardManager;
    private GeneratorManager _generatorManager;
    private UiManager _uiManager;
    private PlayerMechanic _currentPlayer;
    private PlayerMechanic[] _playerMechanics;
    private List<Color> _colors = new List<Color>();
    private float _offset = 0.1f;
    private int _playerIndex;
    private bool _ableToMove = true;

    public override void Awake()
    {
        base.Awake();
        _playerMechanics = new PlayerMechanic[PlayerAmount];
        _colors.Fill(PlayerAmount);
    }

    public override void Start()
    {
        base.Start();

        _boardManager = BoardManager.Instance;
        _uiManager = UiManager.Instance;
        _generatorManager = GeneratorManager.Instance;

        _generatorManager.OnAnimationsDone += () => StartCoroutine(SpawnPlayers());
    }

    public bool CheckTurn(PlayerMechanic playerMechanic) => playerMechanic == _currentPlayer && _ableToMove;

    private IEnumerator SpawnPlayers()
    {
        for (int i = 0; i < _playerMechanics.Length; i++)
        {
            var position = new Vector3(-0.45f + _offset * i, 0.55f, -0.45f);
            var scale = new Vector3(0.0025f, 0.075f, 0.0025f);

            var player = Instantiate(PlayerPrefab, PlayersHolder);
            var playerMechanic = player.GetComponent<PlayerMechanic>();

            player.transform.localScale = scale;
            player.transform.localPosition = position;
            playerMechanic.Sail.material.color = _colors.PickRandom(true);
            _playerMechanics[i] = playerMechanic;

            yield return new WaitForSecondsRealtime(Constants.PLAYER_START_DURATION);
        }

        NextTurn();
        OnPlayersSpawned?.Invoke(_playerMechanics);
    }

    private void NextTurn()
    {
        _currentPlayer = _playerMechanics[_playerIndex];
        _uiManager.StartAnnouncement($"Player {_playerIndex + 1} turn!", 3.0f, 1.0f);
        _playerIndex = (_playerIndex + 1) % PlayerAmount;
        OnTurnStart?.Invoke(_currentPlayer.transform);
    }

    public void MakeTurn(Transform player)
    {
        StartCoroutine(DoTurn(player));
    }

    private IEnumerator DoTurn(Transform player)
    {
        _ableToMove = false;
        var target = _boardManager.SelectedPiece.go.transform.position;
        OnMoveStart?.Invoke(player, target);

        yield return new WaitForSecondsRealtime(Constants.PLAYER_MOVE_DURATION + 1.0f);
        NextTurn();
        yield return new WaitForSecondsRealtime(3.0f);
        
        OnMoveEnd?.Invoke(_currentPlayer.gameObject.transform);
        _ableToMove = true;
    }
}
