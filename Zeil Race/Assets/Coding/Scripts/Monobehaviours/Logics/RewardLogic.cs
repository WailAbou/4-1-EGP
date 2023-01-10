using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IRewardAnimation))]
public class RewardLogic : BaseLogic<IRewardAnimation>
{
    [Header("RewardLogic Refrences")]
    public Transform RewardsHolder;
    public GameObject RewardButtonPrefab;
    public Button MineRewardButton;

    protected override void SetupLogic()
    {
        _playerManager.OnPlayersSpawned += InitOptions;
    }

    protected override void SetupAnimation()
    {
        _rewardManager.OnRewardStart += _animation.SpawnAnimation;
    }

    private void InitOptions(PlayerLogic[] players)
    {
        MineRewardButton.onClick.AddListener(RewardMine);

        foreach (var player in players)
        {
            var playerRewardButton = Instantiate(RewardButtonPrefab, RewardsHolder);
            playerRewardButton.GetComponentInChildren<TMP_Text>().SetText($"Extra zet speler: {player.Name}");
            playerRewardButton.GetComponentInChildren<Button>().onClick.AddListener(() => RewardRoll(player));
        }
    }

    private void RewardMine()
    {
        _rewardManager.RewardMine(_playerManager.CurrentPlayer.transform);
        _animation.CloseAnimation();
    }

    private void RewardRoll(PlayerLogic player)
    {
        _rewardManager.RewardRoll(player.transform);
        _animation.CloseAnimation();
        _rewardManager.EndReward();
    }
}
