using UnityEngine;

[RequireComponent(typeof(IGridAnimation))]
public class GridLogic : BaseLogic<IGridAnimation>
{
    [HideInInspector]
    public GridCell GridCell;
    public QuestionType QuestionType;

    private Vector2 _playerPosition;
    private int _range;
    private bool _playerAbleToMove;
    private bool _interactable => _playerAbleToMove && InRange();

    protected override void SetupLogic()
    {
        _playerManager.OnTurnStart += (_, playerPosition) => _playerPosition = playerPosition;
        _diceManager.OnEndDiceRolls += roll => { _playerAbleToMove = true; _range = roll; };
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

    /// <summary>
    /// Checks if the current grid is in range of the possible moves from the current player.
    /// </summary>
    private bool InRange()
    {
        Vector2 gridPosition = GridCell.position;
        int xCost = (int)Mathf.Abs(_playerPosition.x - gridPosition.x);
        int yCost = (int)Mathf.Abs(_playerPosition.y - gridPosition.y);
        return (xCost + yCost) <= _range;
    }
}

public enum QuestionType { None, Normal, Final };