using UnityEngine;
using System;

public class BoardManager : Singleton<BoardManager>
{
    [Header("BoardManager References")]
    public BoardAnimation BoardAnimation;
    public GridCell[,] GridCells;

    [HideInInspector] 
    public Action<GridCell> OnSelect;
    public Action<GridCell> OnHoverEnter;
    public Action<GridCell> OnHoverLeave;

    private GridCell _hoveredCell;

    public void HoverPiece(GridCell gridCell)
    {
        if (_hoveredCell != null && _hoveredCell != gridCell)
            OnHoverLeave?.Invoke(_hoveredCell);

        _hoveredCell = gridCell;
        OnHoverEnter?.Invoke(_hoveredCell);
    }

    public void SelectPiece(GridCell gridCell)
    {
        OnSelect?.Invoke(gridCell);
    }
}
