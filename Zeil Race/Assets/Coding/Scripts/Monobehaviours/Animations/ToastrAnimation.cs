using DG.Tweening;
using UnityEngine;

public class ToastrAnimation : MonoBehaviour, IToastrAnimation
{
    private GameObject _toastr;
    private RectTransform _toastrPanel;
    private Sequence _sequence;

    private void Awake()
    {
        _toastr = transform.GetChild(0).gameObject;
        _toastrPanel = _toastr.GetComponent<RectTransform>();
    }

    public void StartToastrAnimation(string text)
    {
        _toastrPanel.DOKill();
        _sequence?.Kill();

        _sequence = DOTween.Sequence().SetEase(Ease.InOutQuad);
        _sequence.Append(_toastrPanel.DOScale(Vector3.one, Animations.TOASTR_SPAWN_DURATION / 2));
        _sequence.AppendInterval(Animations.TOASTR_SPAWN_DURATION / 2);
        _sequence.Append(_toastrPanel.DOAnchorPosY(-50, Animations.TOASTR_MOVE_DURATION / 2));
        _sequence.AppendInterval(Animations.TOASTR_MOVE_DURATION / 2);

        _sequence.OnComplete(() => {
            _toastrPanel.DOScale(Vector3.zero, Animations.TOASTR_END_DURATION).SetEase(Ease.InOutQuad).OnComplete(() => _toastr.SetActive(false));
        });
    }
}
