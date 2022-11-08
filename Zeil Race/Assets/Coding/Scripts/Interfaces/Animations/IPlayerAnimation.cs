using UnityEngine;

public interface IPlayerAnimation
{
    public void SpawnAnimation();
    public void MoveStartAnimation(Transform player, Transform target);
}
