using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Transform _target;
    private bool _gameStarted;

    public override void Setup()
    {
        _playerManager.OnPlayersSpawned += _ => _gameStarted = true;
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
        if (!_gameStarted) return;

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
