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

    /// <summary>
    /// Starts the movements animation if the moved player is equal to this player.
    /// </summary>
    /// <param name="player">The current player that made a turn.</param>
    /// <param name="target">The target to where the player wants to go to.</param>
    private void MoveStart(Transform player, Transform target)
    {
        if (player != transform) return;

        _animation.MoveStartAnimation(player, target);
    }
}
