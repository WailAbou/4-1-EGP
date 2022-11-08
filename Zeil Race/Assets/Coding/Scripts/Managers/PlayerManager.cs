using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("GameManager References")]
    public Transform PlayersHolder;
    public GameObject PlayerPrefab;

    [Header("PlayerManager Settings")]
    public int PlayerAmount = 3;

    public Action<Transform> OnTurnStart;
    public Action<Transform, Transform> OnMoveStart;
    public Action<Transform> OnMoveEnd;
    public Action<PlayerMechanic[]> OnPlayersSpawned;

    private GeneratorManager _generatorManager;
    private UiManager _uiManager;
    private PlayerMechanic _currentPlayer;
    private PlayerMechanic[] _playerMechanics;
    private List<Color> _colors = new List<Color>();
    private float _offset = 0.3f;
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

        _uiManager = UiManager.Instance;
        _generatorManager = GeneratorManager.Instance;

        _generatorManager.OnAnimationsDone += (startGrid) => StartCoroutine(SpawnPlayers(startGrid));
    }

    public bool CheckTurn(PlayerMechanic playerMechanic) => playerMechanic == _currentPlayer && _ableToMove;

    private IEnumerator SpawnPlayers(GridPiece startGrid)
    {
        for (int i = 0; i < _playerMechanics.Length; i++)
        {
            var startPos = startGrid.gameObject.transform.position;
            var position = new Vector3(startPos.x + _offset * i, startPos.y, startPos.z);

            var player = Instantiate(PlayerPrefab, PlayersHolder);
            var playerMechanic = player.GetComponent<PlayerMechanic>();

            player.transform.position = position;
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

    public void TakeTurn(Transform player, Transform target)
    {
        StartCoroutine(TakeTurnRoutine(player, target));
    }

    private IEnumerator TakeTurnRoutine(Transform player, Transform target)
    {
        _ableToMove = false;

        OnMoveStart?.Invoke(player, target);
        yield return new WaitForSecondsRealtime(Constants.PLAYER_MOVE_DURATION + 1.0f);
        NextTurn();
        OnMoveEnd?.Invoke(player);

        _ableToMove = true;
    }
}
