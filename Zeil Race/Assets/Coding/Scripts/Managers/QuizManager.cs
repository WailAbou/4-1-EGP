using System;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : Singleton<QuizManager>
{
    [Header("GameManager Settings")]
    public List<QuizScriptableObject> NormalQuizes;
    public List<QuizScriptableObject> FinalQuizes;

    public Action<bool, QuizScriptableObject> OnQuizCorrect;
    public Action<bool, QuizScriptableObject> OnQuizIncorrect;
    public Action<bool, QuizScriptableObject> OnQuizStart;
    public Action<bool> OnQuizEnd;

    private bool _isFinalQuiz;

    public void StartQuiz(bool isFinalQuiz)
    {
        _isFinalQuiz = isFinalQuiz;
        var quiz = _isFinalQuiz ? FinalQuizes.PickRandom() : NormalQuizes.PickRandom();
        OnQuizStart?.Invoke(_isFinalQuiz, quiz);
    }

    public void EndQuiz()
    {
        OnQuizEnd?.Invoke(_isFinalQuiz);
    }
}
