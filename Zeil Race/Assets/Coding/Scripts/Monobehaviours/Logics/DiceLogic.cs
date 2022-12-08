using UnityEngine;

[RequireComponent(typeof(IDiceAnimation))]
public class DiceLogic : BaseLogic<IDiceAnimation>
{
    public GameObject WallsHolder;

    private int _allowedRolls;
    private int _diceIndex;
    private bool _ableToRoll;

    protected override void SetupLogic()
    {
        _diceManager.OnStartDiceRolls += InitDice;
        _diceManager.OnDiceRolled += DiceRolled;
        _diceManager.OnEndDiceRolls += EndDice;
    }

    protected override void SetupAnimation()
    {
        _diceManager.OnStartDiceRolls += _animation.MoveStartAnimation;
        _uiManager.OnEndCoordinates += _animation.MoveEndAnimation;
    }

    private void InitDice(int allowedRolls)
    {
        _diceIndex = 0;
        _allowedRolls = allowedRolls; 
        _ableToRoll = true;
        WallsHolder.SetActive(true);
    }

    private void DiceRolled(int diceIndex)
    {
        _diceIndex = diceIndex;
        _ableToRoll = true;
        _animation.MoveStartAnimation(_allowedRolls);
    }

    private void EndDice(int totalRolled)
    {
        _ableToRoll = false; 
        WallsHolder.SetActive(false);
    }

    private void Update()
    {
        if (_ableToRoll && Input.GetKeyDown(KeyCode.Space))
        {
            _ableToRoll = false;
            _animation.MoveStopAnimation(_diceIndex, diceRoll => _diceManager.EndRollDices(diceRoll));
        }
    }
}
