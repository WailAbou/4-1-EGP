using DG.Tweening;
using UnityEngine;

public class NameAnimation : MonoBehaviour, INameAnimation
{
    [Header("NameAnimation References")]
    public RectTransform NamePanel;

    public void StartAnimation()
    {
        NamePanel.DOAnchorPosY(100, Animations.NAME_SPAWN_DURATION);
    }

    public void StopAnimation(string name)
    {
        NamePanel.DOAnchorPosY(-250, Animations.NAME_END_DURATION);
    }
}
