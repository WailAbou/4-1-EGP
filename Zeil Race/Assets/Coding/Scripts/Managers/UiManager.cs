using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class UiManager : Singleton<UiManager>
{
    [Header("UiManager References")]
    public Transform BoardCoordinatesHolder;
    public TMP_Text CoordinatesDisplay;
    public GameObject SkipButton;

    public Action<string, string> OnStartToastr;
    public Action OnEndToastr;
    public Action OnStartCoordinates;
    public Action<bool> OnEndCoordinates;
    public Action OnStartName;
    public Action<string> OnEndName;

    private Vector2Int _coordinates;

    public override void Setup()
    {
        _diceManager.OnEndDiceRolls += _ => SkipButton.SetActive(false);
        _playerManager.OnTurnStart += DisplayCoordinates;
        _quizManager.OnQuizCorrect += DisplayEndScreen;
    }

    public void StartToastr(string title, string subtitle)
    {
        StartCoroutine(StartToastrRoutine(title, subtitle));
    }

    private IEnumerator StartToastrRoutine(string title, string subtitle)
    {
        OnStartToastr?.Invoke(title, subtitle);
        yield return new WaitForSecondsRealtime(Animations.TOASTR_SPAWN_DURATION + Animations.TOASTR_MOVE_DURATION + Animations.TOASTR_END_DURATION);
        OnEndToastr?.Invoke();
    }

    public void StartCoordinates(Vector2Int coordinates)
    {
        _coordinates = coordinates;
        OnStartCoordinates?.Invoke();
    }

    public void EndCoordinates(Vector2Int coordinates)
    {
        var correct = (coordinates == _coordinates);
        OnEndCoordinates?.Invoke(correct);
    }

    public void StartName()
    {
        OnStartName?.Invoke();
    }

    public void EndName(string name)
    {
        OnEndName?.Invoke(name);
    }

    private void DisplayCoordinates(Transform player, Vector2Int coordinates)
    {
        CoordinatesDisplay.SetText($"Coordinaten: ({coordinates.x} , {coordinates.y})");
        SkipButton.SetActive(true);
    }

    private void DisplayEndScreen(Quiz quiz)
    {
        if (quiz.QuestionType != QuestionType.Final) return;

        StartToastr($"{_playerManager.CurrentPlayer.Name} wint!", "Game Compleet!");
    }
}
