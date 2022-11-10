using System;

public class GridManager : Singleton<GridManager>
{
    public GridCell[,] GridCells;
    public Action<GridCell> OnSelect;
    public Action<GridCell> OnHoverEnter;
    public Action<GridCell> OnHoverLeave;

    private GridCell _hoveredCell;

    public void HoverGridCell(GridCell gridCell)
    {
        DeHoverGridCell(gridCell);
        if (gridCell == null) return;

        _hoveredCell = gridCell;
        OnHoverEnter?.Invoke(_hoveredCell);
    }

    private void DeHoverGridCell(GridCell gridCell)
    {
        if (_hoveredCell != null && _hoveredCell != gridCell)
            OnHoverLeave?.Invoke(_hoveredCell);
    }

    public void SelectGridCell(GridCell gridCell)
    {
        OnSelect?.Invoke(gridCell);
    }
}
