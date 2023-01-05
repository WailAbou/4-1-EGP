using System;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : Singleton<RewardManager>
{
    public Action OnRewardEnd;
    public Action OnRewardStart;

    private List<Transform> _extraRollPlayers = new List<Transform>();
    private bool _rewardMine;

    public void EndReward()
    {
        OnRewardEnd?.Invoke();
        _rewardMine = false;
    }

    public void StartReward()
    {
        OnRewardStart?.Invoke();
    }

    public void RewardRoll(Transform player)
    {
        _extraRollPlayers.Add(player);
    }

    public void RewardMine(Transform player)
    {
        _rewardMine = true;
        _uiManager.StartToastr("Beloning ronde!", "Plaats een mijn");
    }

    public int GetRolls(Transform player)
    {
        var allowedRolls = _extraRollPlayers.Contains(player) ? 2 : 1;
        _extraRollPlayers.Remove(player);
        return allowedRolls;
    }

    public bool GetMine()
    {
        return _rewardMine;
    }
}
