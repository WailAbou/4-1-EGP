using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NameAnimation : MonoBehaviour, INameAnimation
{
    [Header("NameAnimation References")]
    public RectTransform NamePanel;

    public void StartAnimation()
    {
        NamePanel.DOAnchorPosY(100, 1.0f);
    }

    public void StopAnimation(string name)
    {
        NamePanel.DOAnchorPosY(-250, 1.0f);
    }
}
