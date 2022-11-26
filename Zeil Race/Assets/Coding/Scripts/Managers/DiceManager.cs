using System;
using UnityEngine;

public class DiceManager : Singleton<DiceManager>
{
    public Action<int> OnStartDiceRolls;
    public Action<int> OnDiceRolled;
    public Action<int> OnEndDiceRolls;

    private int _allowedRolls = 1;
    private int _diceRoll;
    private int _diceIndex;

    public override void Setup()
    {
        _playerManager.OnTurnStart += StartDiceRolls;
    }

    public void StartDiceRolls(Transform player, Vector2Int gridPosition)
    {
        _diceRoll = 0;
        _diceIndex = 0;
        OnStartDiceRolls?.Invoke(_allowedRolls);
    }

    /// <summary>
    /// Stores the rolled dice number and goes on to the next one or ends the rolls if it is the last one.
    /// </summary>
    /// <param name="diceRoll">The rolled number of the current dice.</param>
    public void EndRollDices(int diceRoll)
    {
        _diceRoll += diceRoll;
        _diceIndex++;

        if (_diceIndex < _allowedRolls) OnDiceRolled?.Invoke(diceRoll);
        else OnEndDiceRolls?.Invoke(_diceRoll);
    }

}
