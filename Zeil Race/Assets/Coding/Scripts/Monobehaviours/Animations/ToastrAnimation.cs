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
        sequence.Append(_toastrPanel.DOScale(Vector3.one, Constants.TOASTR_SPAWN_DURATION / 2));
        sequence.AppendInterval(Constants.TOASTR_SPAWN_DURATION / 2);
        sequence.Append(_toastrPanel.DOAnchorPosY(-50, Constants.TOASTR_MOVE_DURATION));
    }

    public void EndToastrAnimation()
    {
        _toastrPanel.DOScale(Vector3.zero, Constants.TOASTR_END_DURATION).SetEase(Ease.InOutQuad).OnComplete(() => _toastr.SetActive(false));
    }
}
