using System;
using UnityEngine;

public class DiceManager : Singleton<DiceManager>
{
    public Action<int> OnStartDiceRolls;
    public Action<int, bool> OnDiceRolled;
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
        _allowedRolls = _rewardManager.GetAllowedRolls(player);
        _diceRoll = 0;
        _diceIndex = 0;
        OnStartDiceRolls?.Invoke(_allowedRolls);
    }

    public void EndRollDices(int diceRoll)
    {
        _diceIndex++;
        _diceRoll += diceRoll;
        bool endRoll = _diceIndex == _allowedRolls;

        OnDiceRolled?.Invoke(_diceIndex, endRoll);

        if (endRoll)
        {
            _allowedRolls = 1;
            OnEndDiceRolls?.Invoke(_diceRoll);
        } 
    }
}
