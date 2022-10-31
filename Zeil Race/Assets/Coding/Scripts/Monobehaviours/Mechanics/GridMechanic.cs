using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMechanic : MonoBehaviour
{
    private BoardManager _boardManager;
    private GridPiece _piece;

    private void Start()
    {
        _boardManager = BoardManager.Instance;
        _piece = _boardManager.FindPiece(gameObject);
    }

    private void OnMouseEnter()
    {
        _boardManager.HoverPiece(_piece);
    }

    private void OnMouseDown()
    {
        _boardManager.SelectPiece(_piece);
    }
}
