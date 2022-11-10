using UnityEngine;

public class GridCell
{
    public GameObject gameObject;
    public Vector2 position;
    public QuestionType QuestionType;
    public IslandType IslandType = IslandType.Water;
    public bool IsSelected;

    public GridCell(GameObject _gameObject, int _x, int _y)
    {
        gameObject = _gameObject;
        position = new Vector2(_x, _y);

        var gridLogic = _gameObject.GetComponent<GridLogic>();
        gridLogic.GridCell = this;
        QuestionType = gridLogic.QuestionType;
    }
}

public enum IslandType
{
    Water,
    Land
}
