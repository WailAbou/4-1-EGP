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

    public void StartToastrAnimation(string text)
    {
        var sequence = DOTween.Sequence().SetEase(Ease.InOutQuad);
        sequence.Append(_toastrPanel.DOScale(Vector3.one, Constants.TOASTR_MOVE_DURATION / 4.0f));
        sequence.AppendInterval(Constants.TOASTR_MOVE_DURATION / 2.0f);
        sequence.Append(_toastrPanel.DOAnchorPosY(-50, Constants.TOASTR_MOVE_DURATION / 4.0f));
    }

    public void EndToastrAnimation()
    {
        _toastrPanel.DOScale(Vector3.zero, Constants.TOASTR_END_DURATION).SetEase(Ease.InOutQuad).OnComplete(() => _toastr.SetActive(false));
    }
}
