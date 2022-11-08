using UnityEngine;
using System;

public class BoardManager : Singleton<BoardManager>
{
    [Header("BoardManager References")]
    public BoardAnimation BoardAnimation;
    public GridPiece[,] GridPieces;

    public Action<GridPiece> OnSelect;
    public Action<GridPiece> OnHoverEnter;
    public Action<GridPiece> OnHoverLeave;

    private GridPiece _hoverPiece;

    public void HoverPiece(GridPiece gridPiece)
    {
        if (_hoverPiece != null && _hoverPiece != gridPiece)
            OnHoverLeave?.Invoke(_hoverPiece);

        _hoverPiece = gridPiece;
        OnHoverEnter?.Invoke(_hoverPiece);
    }

    public void SelectPiece(GridPiece gridPiece)
    {
        OnSelect?.Invoke(gridPiece);
    }
}
