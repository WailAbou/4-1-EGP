using System;
using UnityEngine;

public class CellManager : Singleton<CellManager>
{
    public CellLogic[,] Cells;
    public Action<CellLogic> OnSelectCell;
    public Action<CellLogic> OnHoverEnterCell;
    public Action<CellLogic> OnHoverLeaveCell;
    public bool CanPlace;

    private CellLogic _hoveredCell;

    public void HoverCell(CellLogic cell)
    {
        if (_hoveredCell != null && _hoveredCell != cell)
            OnHoverLeaveCell?.Invoke(_hoveredCell);

        _hoveredCell = cell;
        OnHoverEnterCell?.Invoke(_hoveredCell);
    }

    public void SelectCell(CellLogic cell)
    {
        OnSelectCell?.Invoke(cell);
    }

    public Vector3 GetPosition(Vector2Int cellCoordinates)
    {
        foreach (var cell in Cells)
        {
            if (cell.Coordinates == cellCoordinates) return cell.transform.position;
        }
        return Vector3.zero;
    }

    public Vector2Int GetCoordinates(Vector3 position)
    {
        float closestDistance = Mathf.Infinity;
        Vector2Int closestCoordinates = Vector2Int.zero;
        foreach (var cell in Cells)
        {
            float distance = Vector3.Distance(cell.transform.position, position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCoordinates = cell.Coordinates;
            }
        }
        return closestCoordinates;
    }
}
