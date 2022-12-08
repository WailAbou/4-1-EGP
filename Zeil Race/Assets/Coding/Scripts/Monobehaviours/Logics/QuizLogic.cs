using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IQuizAnimation))]
public class QuizLogic : BaseLogic<IQuizAnimation>
{
    public GameObject AnswerPrefab;
    public Transform AnswersHolder;
    public TMP_Text QuestionDisplay;

    protected override void SetupLogic()
    {
        _quizManager.OnQuizStart += InitQuiz;
    }

    protected override void SetupAnimation()
    {
        _quizManager.OnQuizStart += _animation.StartQuizAnimation;
        _quizManager.OnQuizEnd += _animation.EndQuizAnimation;
    }

    private void InitQuiz(Quiz quiz)
    {
        QuestionDisplay.SetText(quiz.Question);
        
        foreach (Transform answer in AnswersHolder)
        {
            Destroy(answer.gameObject);
        }

        foreach (var answer in quiz.Answers)
        {
            var answerButton = Instantiate(AnswerPrefab, AnswersHolder);
            answerButton.GetComponentInChildren<TMP_Text>().SetText(answer.Content);
            answerButton.GetComponentInChildren<Button>().onClick.AddListener(() => CheckAnswer(answer));
        }
    }

    private void CheckAnswer(Quiz.Answer answer)
    {
        if (answer.Correct) _quizManager.CorrectAnswer();
        else _quizManager.IncorrectAnswer();
    }
}