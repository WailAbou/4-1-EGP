using UnityEngine;

[RequireComponent(typeof(IDiceAnimation))]
public class DiceLogic : BaseLogic<IDiceAnimation>
{
    [Header("DiceLogic References")]
    public GameObject WallsHolder;

    private int _diceIndex;
    private bool _canRoll;

    protected override void SetupLogic()
    {
        _diceManager.OnStartDiceRolls += InitDice;
        _diceManager.OnDiceRolled += DiceRolled;
        _diceManager.OnEndDiceRolls += EndDice;
    }

    protected override void SetupAnimation()
    {
        _diceManager.OnStartDiceRolls += _ => _animation.MoveStartAnimation();
        _uiManager.OnEndCoordinates += _animation.MoveEndAnimation;
    }

    private void InitDice(int allowedRolls)
    {
        _diceIndex = 0;
        _canRoll = true;
        WallsHolder.SetActive(true);
    }

    private void DiceRolled(int nextDice, bool endRoll)
    {
        _diceIndex = nextDice;
        if (!endRoll) _animation.MoveStartAnimation();
    }

    private void EndDice(int totalRolled)
    {
        _canRoll = false; 
        WallsHolder.SetActive(false);
    }

    private void Update()
    {
        if (_canRoll && Input.GetKeyDown(KeyCode.Space))
        {
            _animation.MoveStopAnimation(_diceIndex, _diceManager.EndRollDices);
        }
    }
}
