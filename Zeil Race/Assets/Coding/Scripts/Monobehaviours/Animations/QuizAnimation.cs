using DG.Tweening;
using UnityEngine;

public class QuizAnimation : MonoBehaviour, IQuizAnimation
{
    private RectTransform _quizPanel;

    private void Awake()
    {
        _quizPanel = transform.GetChild(0).GetComponent<RectTransform>();
    }

    /// <summary>
    /// Slides the quiz panel into the screen.
    /// </summary>
    /// <param name="quiz">The quiz in question.</param>
    public void StartQuizAnimation(Quiz quiz)
    {
        // Center the quiz panel if it is a final quiz otherwise slide it into the side.
        var xPos = quiz.QuestionType == QuestionType.Final ? (Screen.width / 2) - (_quizPanel.sizeDelta.x / 2) : 50;
        _quizPanel.DOAnchorPosX(xPos, Animations.QUIZ_SPAWN_DURATION).SetEase(Ease.InOutQuad);
    }

    public void EndQuizAnimation(Quiz quiz)
    {
        _quizPanel.DOAnchorPosX(-650, Animations.QUIZ_END_DURATION).SetEase(Ease.InOutQuad);
    }
}