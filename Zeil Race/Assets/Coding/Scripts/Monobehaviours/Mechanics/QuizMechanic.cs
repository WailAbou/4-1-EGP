using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizMechanic : MonoBehaviour
{
    [Header("QuizMechanic Reference")]
    public GameObject AnswerPrefab;
    public Transform AnswersHolder;
    public TMP_Text QuestionDisplay;

    private UiManager _uiManager;

    public void Start()
    {
        _uiManager = UiManager.Instance;
        _uiManager.OnQuizStart += SetQuiz;
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
            answerButton.GetComponentInChildren<Button>().onClick.AddListener(() => CheckAnswer(answer));
        }
    }

    private void CheckAnswer(QuizScriptableObject.Answer answer)
    {
        if (answer.Correct) Debug.Log("Correct");
        else Debug.Log("Incorrect");
    }
}