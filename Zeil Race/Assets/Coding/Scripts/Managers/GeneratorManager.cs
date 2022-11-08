using System;
using System.Collections;
using UnityEngine;

public class GeneratorManager : Singleton<GeneratorManager>
{
    [Header("GeneratorManager References")]
    public Transform GridPiecesHolder;
    public GameObject[] GridPiecePrefabs;

    [Header("GeneratorManager Settings")]
    public Vector2Int GridSize = new Vector2Int(10, 10);

    public Action OnGenerateDone;
    public Action<GridPiece> OnAnimationsDone;

    private BoardManager _boardManager;
    public Vector2 _origin;
    private float _gridWith;
    private float _gridHeight;

    public override void Start()
    {
        base.Start();

        _boardManager = BoardManager.Instance;
        _gridWith = 1.0f / GridSize.x;
        _gridHeight = 1.0f / GridSize.y;
        _origin = new Vector2(-(_gridWith * (GridSize.x / 2)) + (_gridWith / 2), -(_gridHeight * (GridSize.y / 2)) + (_gridHeight / 2));
        _boardManager.GridPieces = new GridPiece[GridSize.y, GridSize.x];

        StartCoroutine(GenerateMap());
    }

    private IEnumerator GenerateMap()
    {
        if (GridPiecePrefabs?.Length > 0 && GridPiecesHolder)
        {
            GridPiece startGrid = null;

            for(int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    var gridType = PickGridType(x, y);
                    var gridPiece = SpawnGridPiece(x, y, gridType);
                    if (startGrid == null) startGrid = gridPiece;
                    yield return new WaitForSecondsRealtime(Constants.GRIDPIECE_SPAWN_DELAY);
                }
            }

            yield return new WaitForSecondsRealtime(Constants.GRIDPIECE_START_DURATION);
            OnGenerateDone?.Invoke();
            yield return new WaitForSecondsRealtime(Constants.BOARD_START_DURATION);
            OnAnimationsDone?.Invoke(startGrid);
        }
    }

    private GameObject PickGridType(int x, int y)
    {
        var gridType = GridPiecePrefabs[0];
        return gridType;
    }

    private GridPiece SpawnGridPiece(int x, int y, GameObject islandPiece)
    {
        var spawned = Instantiate(islandPiece, GridPiecesHolder);

        var position = new Vector3(_origin.x + (_gridWith * x), 0.55f, _origin.y + (_gridHeight * y));
        var scale = new Vector3(_gridWith, 0.1f, _gridHeight);

        spawned.transform.localPosition = position;
        spawned.transform.localScale = scale;

        var gridPiece = new GridPiece(spawned, x, y);
        _boardManager.GridPieces[y, x] = gridPiece;

        return gridPiece;
    }
}
