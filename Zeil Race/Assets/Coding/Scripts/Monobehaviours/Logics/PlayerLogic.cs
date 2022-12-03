using UnityEngine;

[RequireComponent(typeof(IPlayerAnimation))]
public class PlayerLogic : BaseLogic<IPlayerAnimation>
{
    public Renderer Sail;
    public string Name;

    protected override void SetupAnimation()
    {
        _playerManager.OnMoveStart += MoveStart;
        _animation.SpawnAnimation();
    }

    private void MoveStart(Transform player, Transform target)
    {
        if (player != transform) return;

        _animation.MoveStartAnimation(player, target);
    }
}
