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
    public Action<string> OnStartCoordinates;
    public Action<bool> OnEndCoordinates;

    private Vector2Int _coordinates;

    public override void Setup()
    {
        _cellManager.OnHoverEnterCell += DisplayCoordinates;
        _quizManager.OnQuizCorrect += DisplayEndScreen;
    }

    public void StartToastr(string text)
    {
        StartCoroutine(StartToastrRoutine(text));
    }

    private IEnumerator StartToastrRoutine(string text)
    {
        OnStartToastr?.Invoke(text);
        yield return new WaitForSecondsRealtime(Animations.TOASTR_SPAWN_DURATION + Animations.TOASTR_MOVE_DURATION + Animations.TOASTR_END_DURATION);
        OnEndToastr?.Invoke();
    }

    public void StartCoordinates(string text, Vector2Int coordinates)
    {
        _coordinates = coordinates;
        OnStartCoordinates?.Invoke(text);
    }

    public void EndCoordinates(Vector2Int coordinates)
    {
        var correct = (coordinates == _coordinates);
        OnEndCoordinates?.Invoke(correct);
    }

    private void DisplayCoordinates(CellLogic cell)
    {
        CoordinatesDisplay.SetText($"Coordinaten: ({cell?.Coordinates.x} , {cell?.Coordinates.y})");
    }

    private void DisplayEndScreen(Quiz quiz)
    {
        if (quiz.QuestionType != QuestionType.Final) return;

        StartToastr("Game Compleet!");
    }
}
