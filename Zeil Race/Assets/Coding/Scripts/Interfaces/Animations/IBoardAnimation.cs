using UnityEngine;

public interface IBoardAnimation
{
    public void SpawnAnimation(CellLogic startCell, CellLogic endCell, Vector2Int gridSize);
}