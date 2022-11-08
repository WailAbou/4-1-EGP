using UnityEngine;

[RequireComponent(typeof(IPlayerAnimation))]
public class PlayerLogic : BaseLogic<IPlayerAnimation>
{
    public Renderer Sail;

    protected override void SetupAnimation()
    {
        _playerManager.OnMoveStart += MoveStart;
        _animation.SpawnAnimation();
    }

    private void MoveStart(Transform player, Transform targert)
    {
        if (player != transform) return;

        _animation.MoveStartAnimation(player, targert);
    }
}
