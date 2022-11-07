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
    public Action OnAnimationsDone;

    private BoardManager _boardManager;
    private Vector2 _origin;
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
            for(int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    var gridPiece = PickGridPiece(x, y);
                    SpawnGridPiece(x, y, gridPiece);
                    yield return new WaitForSecondsRealtime(Constants.GRIDPIECE_SPAWN_DELAY);
                }
            }

            yield return new WaitForSecondsRealtime(Constants.GRIDPIECE_START_DURATION);
            OnGenerateDone?.Invoke();
            yield return new WaitForSecondsRealtime(Constants.BOARD_START_DURATION);
            OnAnimationsDone?.Invoke();
        }
    }

    private GameObject PickGridPiece(int x, int y)
    {
        var islandVarient = GridPiecePrefabs[0];
        return islandVarient;
    }

    private void SpawnGridPiece(int x, int y, GameObject islandPiece)
    {
        var spawned = Instantiate(islandPiece, GridPiecesHolder);

        var position = new Vector3(_origin.x + (_gridWith * x), 0.55f, _origin.y + (_gridHeight * y));
        var scale = new Vector3(_gridWith, 0.1f, _gridHeight);

        spawned.transform.localPosition = position;
        spawned.transform.localScale = scale;

        _boardManager.GridPieces[y, x] = new GridPiece(spawned, x, y);
    }
}
