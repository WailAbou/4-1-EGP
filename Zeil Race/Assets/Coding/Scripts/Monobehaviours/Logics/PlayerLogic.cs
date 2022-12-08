using TMPro;
using UnityEngine;

[RequireComponent(typeof(IPlayerAnimation))]
public class PlayerLogic : BaseLogic<IPlayerAnimation>
{
    [Header("PlayerLogic References")]
    public TMP_Text NameDisplay;
    public Renderer Sail;
    [HideInInspector]
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

    public void SetName(string name)
    {
        Name = name;
        NameDisplay.SetText(name);
    }
}
