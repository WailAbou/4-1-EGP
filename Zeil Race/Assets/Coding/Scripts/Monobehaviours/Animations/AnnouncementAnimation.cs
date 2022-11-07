using DG.Tweening;
using UnityEngine;

public class AnnouncementAnimation : MonoBehaviour
{
    private RectTransform _announcementPanel;
    private UiManager _uiManager;

    private void Awake()
    {
        _announcementPanel = transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void Start()
    {
        _uiManager = UiManager.Instance;
        _uiManager.OnAnnouncementStart += AnnouncementStartAnimation;
        _uiManager.OnAnnouncementEnd += AnnouncementEndAnimation;
    }

    private void AnnouncementStartAnimation(string text, float duration)
    {
        _announcementPanel.localScale = Vector3.zero;

        var sequence = DOTween.Sequence().SetEase(Ease.InOutQuad);
        sequence.Append(_announcementPanel.DOScale(Vector3.one, duration / 4.0f));
        sequence.AppendInterval(duration / 2.0f);
        sequence.Append(_announcementPanel.DOAnchorPosY(-50, duration / 4.0f));
        sequence.Play();
    }

    private void AnnouncementEndAnimation(float closeDuration)
    {
        _announcementPanel.DOScale(Vector3.zero, closeDuration).SetEase(Ease.InOutQuad);
    }
}
