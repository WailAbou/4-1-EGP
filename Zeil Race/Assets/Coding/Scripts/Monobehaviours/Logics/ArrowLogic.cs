using UnityEngine;

[RequireComponent(typeof(IArrowAnimation))]
public class ArrowLogic : BaseLogic<IArrowAnimation>
{
    protected override void SetupLogic()
    {
        _playerManager.OnTurnStart += SetupArrow;
    }

    protected override void SetupAnimation()
    {
        _playerManager.OnPlayersSpawned += _animation.MoveAnimation;
    }

    private void SetupArrow(Transform player, Vector2Int gridPosition)
    {
        transform.position = player.position + new Vector3(0, 0.75f, 0);
        transform.parent = player;
    }
}
