using DG.Tweening;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerManager _playerManager;
    private Vector3 _startScale;

    private void Start()
    {
        _playerManager = PlayerManager.Instance;
        _playerManager.OnMoveStart += OnMoveStartAnimation;

        _startScale = transform.localScale;
        transform.localScale = Vector3.zero;
        StartAnimation();
    }

    public void StartAnimation()
    {
        transform.DOScale(_startScale, Constants.PLAYER_START_DURATION);
    }

    private void OnMoveStartAnimation(Transform player, Transform target)
    {
        if (player != transform) return;

        transform.DOMove(target.position, Constants.PLAYER_MOVE_DURATION);
        transform.DOLookAt(target.position, Constants.PLAYER_MOVE_DURATION / 2);
    }
}
