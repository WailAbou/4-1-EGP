using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Transform _target;

    public override void Setup()
    {
        _cellManager.OnSelectCell += TakeTurn;
        _quizManager.OnQuizCorrect += CorrectAnswer;
        _quizManager.OnQuizIncorrect += IncorrectAnswer;
    }

    /// <summary>
    /// Stores the target and takes the turn if there is no question otherwise start a new quiz.
    /// </summary>
    /// <param name="gridCell">The current selected gridcell to make have the player make a turn to.</param>
    public void TakeTurn(CellLogic cell)
    {
        _target = cell.gameObject.transform;
        if (cell.QuestionType == QuestionType.None) _playerManager.TakeTurn(_target);
        else _quizManager.StartQuiz(cell.QuestionType);
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
