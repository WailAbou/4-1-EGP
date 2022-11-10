using UnityEngine;

[RequireComponent(typeof(IBoardAnimation))]
public class BoardLogic : BaseLogic<IBoardAnimation>
{
    protected override void SetupAnimation()
    {
        _generatorManager.OnGenerateDone += _animation.SpawnAnimation;
    }
}
