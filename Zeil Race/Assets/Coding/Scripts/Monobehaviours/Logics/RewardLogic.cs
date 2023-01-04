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
        //MineRewardButton.onClick.AddListener(() => CheckAnswer(answer));

        foreach (var player in players)
        {
            var playerRewardButton = Instantiate(RewardButtonPrefab, RewardsHolder);
            playerRewardButton.GetComponentInChildren<TMP_Text>().SetText($"Extra zet speler: {player.Name}");
            playerRewardButton.GetComponentInChildren<Button>().onClick.AddListener(() => ExtraRoll(player));
        }
    }

    private void ExtraRoll(PlayerLogic player)
    {
        _diceManager.ExtraRoll(player);
        _animation.CloseAnimation();
        _rewardManager.EndReward();
    }
}
