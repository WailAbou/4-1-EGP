using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizMechanic : MonoBehaviour
{
    [Header("QuizMechanic Reference")]
    public GameObject AnswerPrefab;
    public Transform AnswersHolder;
    public TMP_Text QuestionDisplay;

    private GameManager _gameManager;

    public void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.OnQuizStart += SetQuiz;
    }

    private void SetQuiz(QuizScriptableObject quiz)
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
        if (answer.Correct) _gameManager.OnQuizCorrect?.Invoke(quiz);
        else _gameManager.OnQuizIncorrect?.Invoke(quiz);
    }
}