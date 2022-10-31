using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanic : MonoBehaviour
{
    private GameManager _gameManager;
    private BoardManager _boardManager;

    private bool MyTurn => _gameManager.CurrentPlayer == this;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _boardManager = BoardManager.Instance;
    }

    private void Update()
    {
        if (MyTurn && Input.GetMouseButtonDown(0) && _boardManager.SelectedPiece != null)
            Move();
    }

    private void Move()
    {
        var target = _boardManager.SelectedPiece.go.transform.position;
        transform.DOMove(target, 1.0f);
        transform.DOLookAt(target, 0.5f);
    }
}
