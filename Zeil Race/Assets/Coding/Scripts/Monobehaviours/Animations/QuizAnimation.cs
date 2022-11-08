using DG.Tweening;
using UnityEngine;

public class QuizAnimation : MonoBehaviour, IQuizAnimation
{
    private RectTransform _quizPanel;

    private void Awake()
    {
        _quizPanel = transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void StartQuizAnimation(bool isFinalQuiz, QuizScriptableObject quiz)
    {
        var xPos = isFinalQuiz ? (Screen.width / 2) - (_quizPanel.sizeDelta.x / 2) : 50;
        _quizPanel.DOAnchorPosX(xPos, Constants.QUIZ_SPAWN_DURATION).SetEase(Ease.InOutQuad);
    }

    public void EndQuizAnimation(bool isFinalQuiz)
    {
        _quizPanel.DOAnchorPosX(-650, Constants.QUIZ_END_DURATION).SetEase(Ease.InOutQuad);
    }
}