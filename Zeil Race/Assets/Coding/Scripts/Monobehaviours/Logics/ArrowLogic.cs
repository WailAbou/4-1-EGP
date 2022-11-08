using UnityEngine;

[RequireComponent(typeof(IArrowAnimation))]
public class ArrowLogic : BaseLogic<IArrowAnimation>
{
    protected override void SetupAnimation()
    {
        _playerManager.OnPlayersSpawned += _animation.MoveAnimation;
    }
}
