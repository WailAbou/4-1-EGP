using System;

public class GridManager : Singleton<GridManager>
{
    public GridCell[,] GridCells;
    public Action<GridCell> OnSelectGridCell;
    public Action<GridCell> OnHoverEnterGridCell;
    public Action<GridCell> OnHoverLeaveGridCell;

    private GridCell _hoveredCell;

    public void HoverGridCell(GridCell gridCell)
    {
        if (_hoveredCell != null && _hoveredCell != gridCell)
            OnHoverLeaveGridCell?.Invoke(_hoveredCell);

        _hoveredCell = gridCell;
        OnHoverEnterGridCell?.Invoke(_hoveredCell);
    }

    public void SelectGridCell(GridCell gridCell)
    {
        OnSelectGridCell?.Invoke(gridCell);
    }
}
