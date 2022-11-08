using DG.Tweening;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimation
{
    public void SpawnAnimation()
    {
        var startScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(startScale, Constants.PLAYER_SPAWN_DURATION);
    }

    public void MoveStartAnimation(Transform player, Transform target)
    {
        transform.DOMove(target.position, Constants.PLAYER_MOVE_DURATION);
        transform.DOLookAt(target.position, Constants.PLAYER_MOVE_DURATION / 2);
    }
}
