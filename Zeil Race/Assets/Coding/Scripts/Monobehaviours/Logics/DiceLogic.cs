using UnityEngine;

[RequireComponent(typeof(IDiceAnimation))]
public class DiceLogic : BaseLogic<IDiceAnimation>
{
    public GameObject WallsHolder;

    private int _allowedRolls;
    private int _diceIndex;
    private bool _dicesRolling;

    protected override void SetupLogic()
    {
        _diceManager.OnStartDiceRolls += allowedRolls => { _allowedRolls = allowedRolls; _diceIndex = 0; _dicesRolling = true; WallsHolder.SetActive(true); };
        _diceManager.OnDiceRolled += _ => _diceIndex++;
        _diceManager.OnEndDiceRolls += _ => { _dicesRolling = false; WallsHolder.SetActive(false); };
    }

    protected override void SetupAnimation()
    {
        _diceManager.OnStartDiceRolls += _animation.MoveStartAnimation;
        _gridManager.OnSelectGridCell += (gridCell) => _animation.MoveEndAnimation(gridCell, _allowedRolls);
    }

    /// <summary>
    /// Checks if space is clicked and the dices are rolling, then gets the current diceRoll and ends the roll in the manager.
    /// </summary>
    private void Update()
    {
        if (_dicesRolling && Input.GetKeyDown(KeyCode.Space))
        {
            _animation.MoveStopAnimation(_diceIndex, diceRoll => _diceManager.EndRollDices(diceRoll));
        }
    }
}
