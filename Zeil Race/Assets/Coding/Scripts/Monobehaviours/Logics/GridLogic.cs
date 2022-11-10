using UnityEngine;

[RequireComponent(typeof(IGridAnimation))]
public class GridLogic : BaseLogic<IGridAnimation>
{
    [HideInInspector]
    public GridCell GridCell;
    public QuestionType QuestionType;

    private Vector2 _playerPosition;
    private Vector2 _range;
    private bool _playerAbleToMove;
    private bool _interactable => _playerAbleToMove && InRange(GridCell.position);

    protected override void SetupLogic()
    {
        _playerManager.OnTurnStart += (_, playerPosition) => _playerPosition = playerPosition;
        _diceManager.OnEndDiceRolls += rolls => { _playerAbleToMove = true; _range = rolls; };
        _gridManager.OnSelectGridCell += _ => _playerAbleToMove = false;
    }

    protected override void SetupAnimation()
    {
        _animation.SpawnAnimation();
    }

    private void OnMouseEnter()
    {
        if (!_interactable) return;

        _animation.HoverEnterAnimation(GridCell);
        _gridManager.HoverGridCell(GridCell);
    }

    private void OnMouseExit()
    {
        _animation.HoverLeaveAnimation(GridCell);
    }

    private void OnMouseDown()
    {
        if (!_interactable) return;

        _gridManager.SelectGridCell(GridCell);
    }

    private bool InRange(Vector2 gridPosition)
    {
        bool xInRange = gridPosition.x < _playerPosition.x + _range.x && gridPosition.x > _playerPosition.x - _range.x;
        bool yInRange = gridPosition.y < _playerPosition.y + _range.y && gridPosition.y > _playerPosition.y - _range.y;
        return xInRange && yInRange;
    }
}

public enum QuestionType { None, Normal, Final };