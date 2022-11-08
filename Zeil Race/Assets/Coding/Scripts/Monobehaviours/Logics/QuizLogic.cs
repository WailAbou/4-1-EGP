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
        _quizManager.OnQuizCorrect += CorrectAnswer;
        _quizManager.OnQuizIncorrect += IncorrectAnswer;
    }

    protected override void SetupAnimation()
    {
        _quizManager.OnQuizStart += _animation.StartQuizAnimation;
        _quizManager.OnQuizEnd += _animation.EndQuizAnimation;
    }

    private void InitQuiz(bool isFinalQuiz, QuizScriptableObject quiz)
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
            answerButton.GetComponentInChildren<Button>().onClick.AddListener(() => CheckAnswer(quiz, answer));
        }
    }

    private void CheckAnswer(QuizScriptableObject quiz, QuizScriptableObject.Answer answer)
    {
        if (answer.Correct) _quizManager.OnQuizCorrect?.Invoke(quiz.IsFinalQuiz, quiz);
        else _quizManager.OnQuizIncorrect?.Invoke(quiz.IsFinalQuiz, quiz);
    }

    private void CorrectAnswer(bool isFinalQuiz, QuizScriptableObject quiz)
    {
        Debug.Log("Correct!");
        _quizManager.EndQuiz();
        _playerManager.TakeTurn();
    }

    private void IncorrectAnswer(bool isFinalQuiz, QuizScriptableObject quiz)
    {
        Debug.Log("Incorrect!");
        _quizManager.EndQuiz();
        _playerManager.NextTurn();
    }
}