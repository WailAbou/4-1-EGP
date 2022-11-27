using UnityEngine;

[RequireComponent(typeof(ICellAnimation))]
public class CellLogic : BaseLogic<ICellAnimation>
{
    public Vector2Int Position;
    public QuestionType QuestionType;

    private Vector2Int _playerPosition;
    private int _range;
    private bool _playerAbleToMove;
    private bool _interactable => _playerAbleToMove && InRange();

    protected override void SetupLogic()
    {
        _playerManager.OnTurnStart += (_, playerPosition) => _playerPosition = playerPosition;
        _diceManager.OnEndDiceRolls += roll => { _playerAbleToMove = true; _range = roll; };
        _cellManager.OnSelectCell += _ => _playerAbleToMove = false;
    }

    protected override void SetupAnimation()
    {
        _animation.SpawnAnimation();
    }

    private void OnMouseEnter()
    {
        if (!_interactable) return;

        _animation.HoverEnterAnimation(this);
        _cellManager.HoverCell(this);
    }

    private void OnMouseExit()
    {
        _animation.HoverLeaveAnimation(this);
    }

    private void OnMouseDown()
    {
        if (!_interactable) return;

        _cellManager.SelectCell(this);
    }

    private bool InRange()
    {
        int xCost = Mathf.Abs(_playerPosition.x - Position.x);
        int yCost = Mathf.Abs(_playerPosition.y - Position.y);
        return (xCost + yCost) == _range;
    }
}

public enum QuestionType { None, Normal, Final };