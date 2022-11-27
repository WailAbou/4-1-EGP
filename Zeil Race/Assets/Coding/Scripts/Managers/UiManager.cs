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
        _cellManager.OnHoverEnterCell += DisplayCoordinates;
        _quizManager.OnQuizCorrect += DisplayEndScreen;
    }

    public void StartToastr(string text)
    {
        StartCoroutine(StartToastrRoutine(text));
    }

    /// <summary>
    /// Starts the toastr with the display text waits until it has spawned and moved then ends it.
    /// </summary>
    /// <param name="text">The toastr text to be displayed.</param>
    private IEnumerator StartToastrRoutine(string text)
    {
        OnStartToastr?.Invoke(text);
        yield return new WaitForSecondsRealtime(Animations.TOASTR_SPAWN_DURATION + Animations.TOASTR_MOVE_DURATION);
        OnEndToastr?.Invoke();
    }

    private void DisplayCoordinates(CellLogic cell)
    {
        CoordinatesDisplay.SetText($"Coordinaten: ({cell?.Position.x} , {cell?.Position.y})");
    }

    private void DisplayEndScreen(Quiz quiz)
    {
        if (quiz.QuestionType != QuestionType.Final) return;

        StartToastr("Game Compleet!");
    }
}
