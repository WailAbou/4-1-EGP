using UnityEngine;

public interface ICellAnimation
{
    public void SpawnAnimation(Vector2Int coordinates, Transform boardCoordinatesHolder);
    public void HoverEnterAnimation(CellLogic cell, bool isMine);
    public void HoverLeaveAnimation(CellLogic cell);
}
