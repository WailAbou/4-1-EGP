using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UiManager : Singleton<UiManager>
{
    [Header("UiManager References")]
    public TMP_Text CoordinatesDisplay;
    public QuizScriptableObject QuizScriptableObject;
    public Transform Arrow;

    public Action<string, float> OnAnnouncementStart;
    public Action<float> OnAnnouncementEnd;
    public Action OnAnnouncementStop;

    public Action<QuizScriptableObject> OnQuizStart;
    public Action OnQuizEnd;
    public Action OnQuizStop;

    private BoardManager _boardManager;
    private PlayerManager _playerManager;

    public override void Start()
    {
        base.Start();
        
        _boardManager = BoardManager.Instance;
        _playerManager = PlayerManager.Instance;

        _boardManager.OnHoverEnter += DisplayCoordinates;
        _playerManager.OnTurnStart += DisplayArrow;
        
        StartQuiz(QuizScriptableObject);
    }

    public void StartAnnouncement(string text, float displayDuration, float closeDuration)
    {
        StartCoroutine(DoAnnouncement(text, displayDuration, closeDuration));
    }

    private IEnumerator DoAnnouncement(string text, float displayDuration, float closeDuration)
    {
        OnAnnouncementStart?.Invoke(text, displayDuration);
        yield return new WaitForSecondsRealtime(displayDuration);
        OnAnnouncementEnd?.Invoke(closeDuration);
        yield return new WaitForSecondsRealtime(closeDuration);
        OnAnnouncementStop?.Invoke();
    }

    public void StartQuiz(QuizScriptableObject quiz)
    {
        OnQuizStart?.Invoke(quiz);
    }

    public void EndQuiz()
    {
        StartCoroutine(StopQuiz());
    }

    private IEnumerator StopQuiz()
    {
        OnQuizEnd?.Invoke();
        yield return new WaitForSecondsRealtime(Constants.QUIZ_END_DURATION);
        OnQuizStop?.Invoke();
    }

    private void DisplayCoordinates(GridPiece gridPiece)
    {
        CoordinatesDisplay.SetText($"Coordinaten: ({gridPiece.position.x} , {gridPiece.position.y})");
    }

    private void DisplayArrow(Transform player)
    {
        Arrow.position = player.position + Vector3.up / 2;
        Arrow.parent = player;
    }
}
