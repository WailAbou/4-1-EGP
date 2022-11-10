using UnityEngine;

[RequireComponent(typeof(IDiceAnimation))]
public class DiceLogic : BaseLogic<IDiceAnimation>
{
    private int _diceIndex;
    private bool _dicesRolling;

    protected override void SetupLogic()
    {
        _diceManager.OnStartDiceRolls += () => { _diceIndex = 0; _dicesRolling = true; };
        _diceManager.OnDiceRolled += _ => _diceIndex++;
        _diceManager.OnEndDiceRolls += _ => _dicesRolling = false;
    }

    protected override void SetupAnimation()
    {
        _diceManager.OnStartDiceRolls += _animation.MoveStartAnimation;
        _gridManager.OnSelect += _animation.MoveEndAnimation;
    }

    private void Update()
    {
        if (_dicesRolling && Input.GetKeyDown(KeyCode.Space))
        {
            var diceRoll = _animation.MoveStopAnimation(_diceIndex);
            _diceManager.EndRollDices(diceRoll);
        }
    }
}
