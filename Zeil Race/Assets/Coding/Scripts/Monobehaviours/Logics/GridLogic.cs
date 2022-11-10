using UnityEngine;

[RequireComponent(typeof(IGridAnimation))]
public class GridLogic : BaseLogic<IGridAnimation>
{
    [HideInInspector]
    public GridCell GridCell;
    public QuestionType QuestionType;

    private bool _gameStarted;
    private bool _quizStarted;

    protected override void SetupLogic()
    {
        _playerManager.OnPlayersSpawned += _ => _gameStarted = true;
        _quizManager.OnQuizStart += _ => _quizStarted = true;
        _quizManager.OnQuizEnd += _ => _quizStarted = false;
    }

    protected override void SetupAnimation()
    {
        _boardManager.OnHoverEnter += HoverEnter;
        _boardManager.OnHoverLeave += HoverLeave;
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
        if (!_gameStarted || _quizStarted) return;
        
        _boardManager.HoverPiece(GridCell);
    }

    private void OnMouseDown()
    {
        if (!_gameStarted || _quizStarted) return;

        _boardManager.SelectPiece(GridCell);
    }
}

public enum QuestionType { None, Normal, Final };