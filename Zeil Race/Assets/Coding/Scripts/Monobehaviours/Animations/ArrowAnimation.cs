using DG.Tweening;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    private PlayerManager _playerManager;

    public void Start()
    {
        _playerManager = PlayerManager.Instance;
        _playerManager.OnPlayersSpawned += OnStartAnimation;
    }

    private void OnStartAnimation(PlayerMechanic[] players)
    {
        transform.DOLocalMoveY(transform.localPosition.y - 15f, Constants.ARROW_MOVE_DURATION).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }    
}
