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
    public Action<Quiz> OnQuizCorrect;
    public Action<Quiz> OnQuizIncorrect;

    private Quiz _quiz;

    public void StartQuiz(QuestionType questionType)
    {
        _quiz = (questionType == QuestionType.Final) ? FinalQuizes.PickRandom() : NormalQuizes.PickRandom();
        OnQuizStart?.Invoke(_quiz);
    }

    public void EndQuiz()
    {
        OnQuizEnd?.Invoke(_quiz);
    }

    public void CorrectAnswer()
    {
        OnQuizCorrect?.Invoke(_quiz);
    }

    public void IncorrectAnswer()
    {
        OnQuizIncorrect?.Invoke(_quiz);
    }
}
