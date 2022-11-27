using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour, IArrowAnimation
{
    public void MoveAnimation(List<PlayerLogic> players)
    {
        transform.DOLocalMoveY(transform.localPosition.y - 15f, Animations.ARROW_MOVE_DURATION).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }    
}
