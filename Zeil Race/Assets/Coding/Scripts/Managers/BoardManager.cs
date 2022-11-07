using UnityEngine;
using System.Linq;
using System;

public class BoardManager : Singleton<BoardManager>
{
    [Header("BoardManager References")]
    public BoardAnimation BoardAnimation;
    public GridPiece[,] GridPieces;
    public GridPiece SelectedPiece;

    public Action<GridPiece> OnHoverEnter;
    public Action<GridPiece> OnHoverLeave;

    private GridPiece _hoverPiece;

    public GridPiece FindPiece(GameObject go) => GridPieces.Cast<GridPiece>().Where(gp => gp.go == go).FirstOrDefault();

    public void HoverPiece(GridPiece piece)
    {
        if (_hoverPiece != null && _hoverPiece != piece)
            OnHoverLeave?.Invoke(_hoverPiece);

        _hoverPiece = piece;
        OnHoverEnter?.Invoke(_hoverPiece);
    }

    public void SelectPiece(GridPiece piece)
    {
        SelectedPiece = piece;
    }
}
