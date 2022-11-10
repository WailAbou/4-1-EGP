using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public override void Setup()
    {
        _boardManager.OnSelect += TakeTurn;
        _quizManager.OnQuizCorrect += CorrectAnswer;
        _quizManager.OnQuizIncorrect += IncorrectAnswer;
    }

    public void TakeTurn(GridCell gridCell)
    {
        var target = gridCell.gameObject.transform;

        if (gridCell.QuestionType == QuestionType.None) _playerManager.TakeTurn(target); 
        else _quizManager.StartQuiz(gridCell.QuestionType, target);
    }

    private void CorrectAnswer(Transform target)
    {
        Debug.Log("Correct!");
        _quizManager.EndQuiz();
        _playerManager.TakeTurn(target);
    }

    private void IncorrectAnswer()
    {
        Debug.Log("Incorrect!");
        _quizManager.EndQuiz();
        _playerManager.NextTurn();
    }
}
