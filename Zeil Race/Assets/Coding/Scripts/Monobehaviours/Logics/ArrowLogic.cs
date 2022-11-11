using UnityEngine;

[RequireComponent(typeof(IArrowAnimation))]
public class ArrowLogic : BaseLogic<IArrowAnimation>
{
    protected override void SetupLogic()
    {
        _playerManager.OnTurnStart += DisplayArrow;
    }

    protected override void SetupAnimation()
    {
        _playerManager.OnPlayersSpawned += _animation.MoveAnimation;
    }

    /// <summary>
    /// Displaying the arrow above the player after every new turn.
    /// </summary>
    /// <param name="player">The player of this new turn.</param>
    private void DisplayArrow(Transform player, Vector2Int gridPosition)
    {
        transform.position = player.position + Vector3.up / 2;
        transform.parent = player;
    }
}
