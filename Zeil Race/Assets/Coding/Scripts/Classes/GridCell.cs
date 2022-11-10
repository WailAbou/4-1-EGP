using UnityEngine;

public class GridCell
{
    public GameObject gameObject;
    public Vector2Int position;
    public QuestionType QuestionType;

    public GridCell(GameObject _gameObject, int _x, int _y)
    {
        gameObject = _gameObject;
        position = new Vector2Int(_x, _y);

        var gridLogic = _gameObject.GetComponent<GridLogic>();
        gridLogic.GridCell = this;
        QuestionType = gridLogic.QuestionType;
    }
}