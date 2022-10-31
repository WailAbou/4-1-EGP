using UnityEngine;

public class GridPiece
{
    public GameObject go = null;
    public Vector2 position = Vector3.zero;
    public IslandType IslandType = IslandType.Water;
    public bool IsSelected = false;
    public GridAnimation Anim;

    public GridPiece(GameObject _go, int _x, int _y)
    {
        go = _go;
        position = new Vector2(_x, _y);
        Anim = go.GetComponent<GridAnimation>();
    }
}

public enum IslandType
{
    Water,
    Land
}
