using DG.Tweening;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Vector3 _startScale;

    private void Start()
    {
        _startScale = transform.localScale;
        transform.localScale = Vector3.zero;
        OnStartAnimation();
    }

    public void OnStartAnimation()
    {
        transform.DOScale(_startScale, Constants.PLAYER_START_DURATION);
    }
}
