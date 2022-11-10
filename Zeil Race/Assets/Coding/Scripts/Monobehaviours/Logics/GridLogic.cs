using UnityEngine;

[RequireComponent(typeof(IGridAnimation))]
public class GridLogic : BaseLogic<IGridAnimation>
{
    [HideInInspector]
    public GridCell GridCell;
    public QuestionType QuestionType;

    private Vector2 _playerPosition;
    private Vector2 _range;
    private bool _interactable;

    protected override void SetupLogic()
    {
        _playerManager.OnTurnStart += (_, playerPosition) => _playerPosition = playerPosition;
        _diceManager.OnEndDiceRolls += rolls => { _interactable = true; _range = rolls; };
        _gridManager.OnSelect += _ => _interactable = false;
    }

    protected override void SetupAnimation()
    {
        _gridManager.OnHoverEnter += HoverEnter;
        _gridManager.OnHoverLeave += HoverLeave;
        _animation.SpawnAnimation();
    }

    private void HoverEnter(GridCell gridCell)
    {
        if (gridCell.gameObject != gameObject) return;

        _animation.HoverEnterAnimation(gridCell);
    }

    private void HoverLeave(GridCell gridCell)
    {
        if (gridCell.gameObject != gameObject) return;

        _animation.HoverLeaveAnimation(gridCell);
    }

    private void OnMouseEnter()
    {
        if (!_interactable || !InRange(GridCell.position)) return;

        _gridManager.HoverGridCell(GridCell);
    }

    private void OnMouseDown()
    {
        if (!_interactable || !InRange(GridCell.position)) return;

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