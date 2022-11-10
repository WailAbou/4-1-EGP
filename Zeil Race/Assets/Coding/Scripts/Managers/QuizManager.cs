using System;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : Singleton<QuizManager>
{
    [Header("QuizManager Settings")]
    public List<Quiz> NormalQuizes;
    public List<Quiz> FinalQuizes;

    public Action<Quiz> OnQuizStart;
    public Action<Quiz> OnQuizEnd;
    public Action<Transform> OnQuizCorrect;
    public Action OnQuizIncorrect;

    private Transform _target;
    private Quiz _quiz;

    public void StartQuiz(QuestionType questionType, Transform target)
    {
        _target = target;
        _quiz = questionType == QuestionType.Final ? FinalQuizes.PickRandom() : NormalQuizes.PickRandom();
        OnQuizStart?.Invoke(_quiz);
    }

    public void EndQuiz()
    {
        OnQuizEnd?.Invoke(_quiz);
    }

    public void CorrectAnswer()
    {
        OnQuizCorrect?.Invoke(_target);
    }

    public void IncorrectAnswer()
    {
        OnQuizIncorrect?.Invoke();
    }
}
