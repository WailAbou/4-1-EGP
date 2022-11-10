using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratorManager : Singleton<GeneratorManager>
{
    [Header("GeneratorManager References")]
    public Transform GridPiecesHolder;
    public GameObject EndGridPiecePrefab;
    public List<GameObject> GridPiecePrefabs;

    [Header("GeneratorManager Settings")]
    public Vector2Int GridSize = new Vector2Int(10, 10);

    public Action OnGenerateDone;
    public Action<GridCell> OnAnimationsDone;

    public Vector2 _origin;
    private float _gridWith;
    private float _gridHeight;

    public override void Setup()
    {
        _gridWith = 1.0f / GridSize.x;
        _gridHeight = 1.0f / GridSize.y;
        _origin = new Vector2(-(_gridWith * (GridSize.x / 2)) + (_gridWith / 2), -(_gridHeight * (GridSize.y / 2)) + (_gridHeight / 2));
        _boardManager.GridCells = new GridCell[GridSize.y, GridSize.x];

        StartCoroutine(GenerateMap());
    }

    private IEnumerator GenerateMap()
    {
        if (GridPiecePrefabs?.Count > 0 && GridPiecesHolder)
        {
            GridCell startGridCell = null;
            var finalX = Random.Range((int)Math.Ceiling(GridSize.x * 0.75f), GridSize.x);
            var finalY = Random.Range((int)Math.Ceiling(GridSize.y * 0.75f), GridSize.y);

            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    var gridCellType = (x == finalX && y == finalY) ? EndGridPiecePrefab : GetGridCellType(x, y);
                    var gridCell = SpawnGridCell(x, y, gridCellType);
                    if (startGridCell == null) startGridCell = gridCell;
                    yield return new WaitForSecondsRealtime(Constants.GRIDPIECE_SPAWN_DELAY);
                }
            }

            yield return new WaitForSecondsRealtime(Constants.GRIDPIECE_SPAWN_DURATION);
            OnGenerateDone?.Invoke();
            yield return new WaitForSecondsRealtime(Constants.BOARD_SPAWN_DURATION);
            OnAnimationsDone?.Invoke(startGridCell);
        }
    }

    private GameObject GetGridCellType(int x, int y)
    {
        var gridCellPrefab = GridPiecePrefabs.PickRandom();
        return gridCellPrefab;
    }

    private GridCell SpawnGridCell(int x, int y, GameObject gridCellPrefab)
    {
        var spawnedGridCell = Instantiate(gridCellPrefab, GridPiecesHolder);

        var position = new Vector3(_origin.x + (_gridWith * x), 0.55f, _origin.y + (_gridHeight * y));
        var scale = new Vector3(_gridWith, 0.1f, _gridHeight);

        spawnedGridCell.transform.localPosition = position;
        spawnedGridCell.transform.localScale = scale;

        var gridCell = new GridCell(spawnedGridCell, x, y);
        _boardManager.GridCells[y, x] = gridCell;

        return gridCell;
    }
}
