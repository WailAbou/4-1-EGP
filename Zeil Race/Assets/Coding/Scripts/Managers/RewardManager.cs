using System;

public class RewardManager : Singleton<RewardManager>
{
    public Action OnRewardEnd;
    public Action OnRewardStart;

    public void EndReward()
    {
        OnRewardEnd?.Invoke();
    }

    public void StartReward()
    {
        OnRewardStart?.Invoke();
    }
}
