using DG.Tweening;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimation
{
    public void SpawnAnimation()
    {
        var startScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(startScale, Animations.PLAYER_SPAWN_DURATION);
    }

    public void MoveStartAnimation(Transform player, Transform target)
    {
        transform.DOMove(target.position, Animations.PLAYER_MOVE_DURATION);
        transform.DOLookAt(target.position, Animations.PLAYER_MOVE_DURATION / 2);
    }
}
