using System.Collections;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    [Header("GameManager References")]
    public Transform PlayersHolder;
    public GameObject PlayerPrefab;

    [Header("GameManager Settings")]
    public int PlayerAmount = 3;
    [HideInInspector]
    public PlayerMechanic CurrentPlayer;

    public Action OnGameStart;

    private PlayerMechanic[] _playerMechanics;
    private int _playerIndex;
    private float _offset = 0.1f;

    public override void Start()
    {
        base.Start();
        GeneratorManager.Instance.OnAnimationsDone += () => StartCoroutine(Setup());
    }

    private IEnumerator Setup()
    {
        _playerMechanics = new PlayerMechanic[PlayerAmount];
        for (int i = 0; i < _playerMechanics.Length; i++)
        {
            var position = new Vector3(-0.45f + _offset * i, 1.5f, -0.45f);
            var scale = new Vector3(0.0025f, 0.075f, 0.0025f);

            var player = Instantiate(PlayerPrefab, PlayersHolder);
            player.transform.localScale = scale;
            player.transform.localPosition = position;
            _playerMechanics[i] = player.GetComponent<PlayerMechanic>();

            yield return new WaitForSecondsRealtime(Constants.PLAYER_START_DURATION);
        }

        StartGame();
    }

    private void StartGame()
    {
        CurrentPlayer = _playerMechanics[_playerIndex];
        OnGameStart?.Invoke();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) CurrentPlayer = _playerMechanics[_playerIndex++];
    }
}
