using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : Singleton<GeneratorManager>
{
    [Header("GeneratorManager References")]
    public Transform GridPiecesHolder;
    public GameObject EndGridPiecePrefab;
    public List<GameObject> GridPiecePrefabs;
    public List<Level> Levels;

    public Action<CellLogic, CellLogic, Vector2Int> OnGenerateDone;
    public Action<CellLogic, Level> OnAnimationsDone;

    private Vector2Int _gridSize = new Vector2Int(10, 10);
    private Vector2 _origin;
    private float _gridWith;
    private float _gridHeight;
    private int _currentLevel;

    public override void Setup()
    {
        _gridWith = 1.0f / _gridSize.x;
        _gridHeight = 1.0f / _gridSize.y;
        _origin = new Vector2(-(_gridWith * (_gridSize.x / 2)) + (_gridWith / 2), -(_gridHeight * (_gridSize.y / 2)) + (_gridHeight / 2));
        _cellManager.Cells = new CellLogic[_gridSize.y, _gridSize.x];

        StartCoroutine(GenerateGrid());
    }

    private IEnumerator GenerateGrid()
    {
        CellLogic endCell = null;
        CellLogic startCell = null;

        for (int y = 0; y < _gridSize.y; y++)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                var gridCellPrefab = GetGridCellPrefab(x, y);
                endCell = SpawnGridCell(x, y, gridCellPrefab);
                if (startCell == null) startCell = endCell;
                yield return new WaitForSecondsRealtime(Animations.GRIDPIECE_SPAWN_DELAY);
            }
        }

        yield return new WaitForSecondsRealtime(Animations.GRIDPIECE_SPAWN_DURATION);
        OnGenerateDone?.Invoke(startCell, endCell, _gridSize);
        yield return new WaitForSecondsRealtime(Animations.BOARD_SPAWN_DURATION);
        OnAnimationsDone?.Invoke(startCell, Levels[_currentLevel]);
    }

    private GameObject GetGridCellPrefab(int x, int y)
    {
        var celLType = Levels[_currentLevel].CellTypes[x + (10 * y)];
        switch (celLType)
        {
            case CellType.Water:
                return GridPiecePrefabs[0];

            case CellType.Land:
                return GridPiecePrefabs[1];

            case CellType.Final:
                return EndGridPiecePrefab;

            default:
                return GridPiecePrefabs[1];
        }
    }
    
    private CellLogic SpawnGridCell(int x, int y, GameObject gridCellPrefab)
    {
        var gridCell = Instantiate(gridCellPrefab, GridPiecesHolder).GetComponent<CellLogic>();
        var position = new Vector3(_origin.x + (_gridWith * x), 0.55f, _origin.y + (_gridHeight * y));
        var scale = new Vector3(_gridWith, 0.1f, _gridHeight);

        gridCell.transform.localPosition = position;
        gridCell.transform.localScale = scale;
        gridCell.Coordinates = new Vector2Int(x, y);
        _cellManager.Cells[y, x] = gridCell;

        return gridCell;
    }
}
