using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("GameManager Settings")]
    public List<QuizScriptableObject> Quizes;

    public Action<QuizScriptableObject> OnQuizStart;
    public Action OnQuizEnd;
    public Action OnQuizStop;

    public Action<QuizScriptableObject> OnQuizCorrect;
    public Action<QuizScriptableObject> OnQuizIncorrect;

    public void StartQuiz()
    {
        var quiz = Quizes.PickRandom();
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
}
