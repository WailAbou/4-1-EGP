using UnityEngine;

public interface ICellAnimation
{
    public void SpawnAnimation(Vector2Int coordinates);
    public void HoverEnterAnimation(CellLogic cell);
    public void HoverLeaveAnimation(CellLogic cell);
    public void DisplayCoordinates(int camIndex);
}
