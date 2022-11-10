using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class UiManager : Singleton<UiManager>
{
    [Header("UiManager References")]
    public TMP_Text CoordinatesDisplay;

    public Action<string> OnStartToastr;
    public Action OnEndToastr;

    public override void Setup()
    {
        _gridManager.OnHoverEnter += DisplayCoordinates;
        _quizManager.OnQuizCorrect += DisplayEndScreen;
    }

    public void StartToastr(string text)
    {
        StartCoroutine(StartToastrRoutine(text));
    }

    private IEnumerator StartToastrRoutine(string text)
    {
        OnStartToastr?.Invoke(text);
        yield return new WaitForSecondsRealtime(Constants.TOASTR_MOVE_DURATION);
        OnEndToastr?.Invoke();
    }

    private void DisplayCoordinates(GridCell gridCell)
    {
        CoordinatesDisplay.SetText($"Coordinaten: ({gridCell.position.x} , {gridCell.position.y})");
    }

    private void DisplayEndScreen(Quiz quiz)
    {
        if (quiz.QuestionType != QuestionType.Final) return;

        StartToastr("Game Compleet!");
    }
}
