using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanic : MonoBehaviour
{
    public Renderer Sail;

    private PlayerManager _playerManager;
    private BoardManager _boardManager;

    private void Start()
    {
        _playerManager = PlayerManager.Instance;
        _boardManager = BoardManager.Instance;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)  && _boardManager.SelectedPiece != null && _playerManager.CheckTurn(this))
            _playerManager.MakeTurn(transform);
    }
}
