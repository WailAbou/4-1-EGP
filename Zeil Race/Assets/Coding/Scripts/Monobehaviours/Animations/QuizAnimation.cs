using DG.Tweening;
using UnityEngine;

public class QuizAnimation : MonoBehaviour
{
    private RectTransform _quizPanel;
    private UiManager _uiManager;

    private void Awake()
    {
        _quizPanel = transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void Start()
    {
        _uiManager = UiManager.Instance;
        _uiManager.OnQuizStart += OnStartAnimation;
    }

    private void OnStartAnimation(QuizScriptableObject quiz)
    {
        _quizPanel.DOAnchorPosX(50, Constants.QUIZ_START_DURATION).SetEase(Ease.InOutQuad);
    }
}