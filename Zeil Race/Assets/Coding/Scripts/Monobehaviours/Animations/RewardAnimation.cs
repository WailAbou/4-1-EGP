using DG.Tweening;
using UnityEngine;

public class RewardAnimation : MonoBehaviour, IRewardAnimation
{
    [Header("RewardAnimation Refrences")]
    public Transform RewardPanel;

    public void SpawnAnimation()
    {
        RewardPanel.DOScale(1, Animations.REWARD_SPAWN_DURATION);
    }

    public void CloseAnimation()
    {
        RewardPanel.DOScale(0, Animations.REWARD_END_DURATION);
    }
}
