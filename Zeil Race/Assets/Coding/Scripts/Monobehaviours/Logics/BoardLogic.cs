using UnityEngine;

[RequireComponent(typeof(IBoardAnimation))]
public class BoardLogic : BaseLogic<IBoardAnimation>
{
    protected override void SetupAnimation()
    {
        GeneratorManager.Instance.OnGenerateDone += _animation.SpawnAnimation;
    }
}
