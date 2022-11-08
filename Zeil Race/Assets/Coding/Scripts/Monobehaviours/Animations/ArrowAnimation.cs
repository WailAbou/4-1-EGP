using DG.Tweening;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour, IArrowAnimation
{
    public void MoveAnimation(PlayerLogic[] players)
    {
        transform.DOLocalMoveY(transform.localPosition.y - 15f, Constants.ARROW_MOVE_DURATION).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }    
}
