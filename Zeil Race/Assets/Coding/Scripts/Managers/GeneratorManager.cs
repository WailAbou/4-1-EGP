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
    public List<Level> Levels;

    public Action OnGenerateDone;
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
        CellLogic startCell = null;
        var finalX = Random.Range((int)Math.Ceiling(_gridSize.x * 0.75f), _gridSize.x);
        var finalY = Random.Range((int)Math.Ceiling(_gridSize.y * 0.75f), _gridSize.y);
            
        for (int y = 0; y < _gridSize.y; y++)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                var gridCellPrefab= (x == finalX && y == finalY) ? EndGridPiecePrefab : GetGridCellPrefab(x, y);
                var gridCell = SpawnGridCell(x, y, gridCellPrefab);
                if (startCell == null) startCell = gridCell;
                yield return new WaitForSecondsRealtime(Animations.GRIDPIECE_SPAWN_DELAY);
            }
        }

        yield return new WaitForSecondsRealtime(Animations.GRIDPIECE_SPAWN_DURATION);
        OnGenerateDone?.Invoke();
        yield return new WaitForSecondsRealtime(Animations.BOARD_SPAWN_DURATION);
        OnAnimationsDone?.Invoke(startCell, Levels[_currentLevel]);
    }

    private GameObject GetGridCellPrefab(int x, int y)
    {
        var islandChance = Random.Range(0, x + y);
        var gridCellPrefab = islandChance > 2 ? GridPiecePrefabs[1] : GridPiecePrefabs[0];
        return gridCellPrefab;
    }
    
    private CellLogic SpawnGridCell(int x, int y, GameObject gridCellPrefab)
    {
        var gridCell = Instantiate(gridCellPrefab, GridPiecesHolder).GetComponent<CellLogic>();
        var position = new Vector3(_origin.x + (_gridWith * x), 0.55f, _origin.y + (_gridHeight * y));
        var scale = new Vector3(_gridWith, 0.1f, _gridHeight);

        gridCell.transform.localPosition = position;
        gridCell.transform.localScale = scale;
        gridCell.Position = new Vector2Int(x, y);
        _cellManager.Cells[y, x] = gridCell;

        return gridCell;
    }
}
