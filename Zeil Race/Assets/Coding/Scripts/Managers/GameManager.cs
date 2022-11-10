using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GridCell _gridCell;
    private Transform _target;

    public override void Setup()
    {
        _gridManager.OnSelectGridCell += TakeTurn;
        _quizManager.OnQuizCorrect += CorrectAnswer;
        _quizManager.OnQuizIncorrect += IncorrectAnswer;
    }

    public void TakeTurn(GridCell gridCell)
    {
        _gridCell = gridCell;
        _target = _gridCell.gameObject.transform;

        if (_gridCell.QuestionType == QuestionType.None) _playerManager.TakeTurn(_target);
        else _quizManager.StartQuiz(_gridCell.QuestionType);
    }

    private void CorrectAnswer(Quiz quiz)
    {
        _quizManager.EndQuiz();
        _playerManager.TakeTurn(_target);
    }

    private void IncorrectAnswer(Quiz quiz)
    {
        _quizManager.EndQuiz();
        _playerManager.NextTurn();
    }
}
