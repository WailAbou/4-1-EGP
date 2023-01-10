using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private CellLogic _cell;
    private Transform _target;
    private bool _gameStarted;

    public override void Setup()
    {
        _playerManager.OnPlayersSpawned += _ => _gameStarted = true;
        _cellManager.OnSelectCell += InputCoordinates;
        _uiManager.OnEndCoordinates += correct => StartCoroutine(TakeTurn(correct));
        
        _quizManager.OnQuizCorrect += CorrectAnswer;
        _quizManager.OnQuizIncorrect += IncorrectAnswer;
        _rewardManager.OnRewardEnd += () => _playerManager.TakeTurn(_target);
    }

    private void InputCoordinates(CellLogic cell)
    {
        if (!_gameStarted) return;

        if (_rewardManager.CanPlaceMine())
        {
            cell.IsMine = true;
            _rewardManager.EndReward();
            return;
        }

        _cell = cell;
        _uiManager.StartCoordinates(cell.Coordinates);
    }

    private IEnumerator TakeTurn(bool correct)
    {
        if (!_gameStarted) yield return null;

        if (correct)
        {
            _target = _cell.gameObject.transform;
            if (_cell.QuestionType == QuestionType.None) _playerManager.TakeTurn(_target);
            else _quizManager.StartQuiz(_cell.QuestionType);
        } 
        else
        {
            yield return new WaitForEndOfFrame();
            _playerManager.NextTurn();
        }
    }

    private void CorrectAnswer(Quiz quiz)
    {
        _quizManager.EndQuiz();
        _rewardManager.StartReward();
    }

    private void IncorrectAnswer(Quiz quiz)
    {
        _quizManager.EndQuiz();
        _playerManager.NextTurn();
    }
}
