using DG.Tweening;
using UnityEngine;

public class ToastrAnimation : MonoBehaviour, IToastrAnimation
{
    private GameObject _toastr;
    private RectTransform _toastrPanel;

    private void Awake()
    {
        _toastr = transform.GetChild(0).gameObject;
        _toastrPanel = _toastr.GetComponent<RectTransform>();
    }

    /// <summary>
    /// Scales the toastr in waits for a moment and moves it to the top.
    /// </summary>
    public void StartToastrAnimation(string text)
    {
        var sequence = DOTween.Sequence().SetEase(Ease.InOutQuad);
        sequence.Append(_toastrPanel.DOScale(Vector3.one, Animations.TOASTR_SPAWN_DURATION / 2));
        sequence.AppendInterval(Animations.TOASTR_SPAWN_DURATION / 2);
        sequence.Append(_toastrPanel.DOAnchorPosY(-50, Animations.TOASTR_MOVE_DURATION));
    }

    /// <summary>
    /// Scaling the toastr out and disabling it if is done.
    /// </summary>
    public void EndToastrAnimation()
    {
        _toastrPanel.DOScale(Vector3.zero, Animations.TOASTR_END_DURATION).SetEase(Ease.InOutQuad).OnComplete(() => _toastr.SetActive(false));
    }
}
