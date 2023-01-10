using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ICellAnimation))]
public class CellLogic : BaseLogic<ICellAnimation>
{
    [HideInInspector]
    public Vector2Int Coordinates;
    public CellType CellType;
    public QuestionType QuestionType;
    public bool IsMine;

    private Vector2Int _currentPlayerCoordinates;
    private int _range;

    private bool _isInteractable
    {
        get
        {
            bool validTurn = InTurnRange() && _playerManager.CanPlace;
            return (validTurn || _cellManager.CanPlace || _rewardManager.CanPlaceMine()) && _playerManager.EmptyCell(this);
        }
    }

    protected override void SetupLogic()
    {
        _playerManager.OnTurnStart += (_, currentPlayerCoordinates) => _currentPlayerCoordinates = currentPlayerCoordinates;
        _diceManager.OnEndDiceRolls += roll => _range = roll;
    }

    protected override void SetupAnimation()
    {
        _animation.SpawnAnimation(Coordinates, _uiManager.BoardCoordinatesHolder);
    }

    private void OnMouseEnter()
    {
        if (!_isInteractable) return;

        _animation.HoverEnterAnimation(this, _rewardManager.CanPlaceMine());
        _cellManager.HoverCell(this);
    }

    private void OnMouseExit()
    {
        _animation.HoverLeaveAnimation(this);
    }

    private void OnMouseDown()
    {
        if (!_isInteractable) return;

        if (!IsMine) _cellManager.SelectCell(this);
        else StartCoroutine(ExplodeMine());
    }

    private bool InTurnRange()
    {
        int xCost = Mathf.Abs(_currentPlayerCoordinates.x - Coordinates.x);
        int yCost = Mathf.Abs(_currentPlayerCoordinates.y - Coordinates.y);
        return (xCost + yCost) == _range || _range == 0;
    }

    private IEnumerator ExplodeMine()
    {
        IsMine = false;
        _uiManager.StartToastr("Oops!", "Een mijn is geëxplodeerd!");
        yield return new WaitForSeconds(3);
        _playerManager.NextTurn();
    }
}

public enum QuestionType { None, Normal, Final };

public enum CellType { Water, Land, Final };