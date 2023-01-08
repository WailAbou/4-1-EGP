using System.Collections;
using UnityEditor;
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
    private bool _playerAbleToMove;
    private bool _interactable => (_playerAbleToMove && !_playerManager.HasPlayer(Coordinates) && InRange()) || _rewardManager.GetMine();

    protected override void SetupLogic()
    {
        _playerManager.OnTurnStart += (_, currentPlayerCoordinates) => _currentPlayerCoordinates = currentPlayerCoordinates;
        _diceManager.OnEndDiceRolls += roll => { _playerAbleToMove = true; _range = roll; };
        _cellManager.OnSelectCell += _ => _playerAbleToMove = false;
    }

    protected override void SetupAnimation()
    {
        _animation.SpawnAnimation(Coordinates, _uiManager.BoardCoordinatesHolder);
    }

    private void OnMouseEnter()
    {
        if (!_interactable) return;

        _animation.HoverEnterAnimation(this, _rewardManager.GetMine());
        _cellManager.HoverCell(this);
    }

    private void OnMouseExit()
    {
        _animation.HoverLeaveAnimation(this);
    }

    private void OnMouseDown()
    {
        if (!_interactable) return;

        if (!IsMine) _cellManager.SelectCell(this);
        else StartCoroutine(ExplodeMine());
    }

    private bool InRange()
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