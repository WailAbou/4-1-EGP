using System;
using UnityEngine;

public class DiceManager : Singleton<DiceManager>
{
    public Action OnStartDiceRolls;
    public Action<int> OnDiceRolled;
    public Action<Vector2Int> OnEndDiceRolls;

    private int[] _diceRolls = new int[2];
    private int _diceIndex;

    public override void Setup()
    {
        _playerManager.OnTurnStart += StartDiceRolls;
    }

    public void StartDiceRolls(Transform player, Vector2Int gridPosition)
    {
        _diceIndex = 0;
        OnStartDiceRolls?.Invoke();
    }

    public void EndRollDices(int diceRoll)
    {
        _diceRolls[_diceIndex] = diceRoll;
        _diceIndex++;

        if (_diceIndex < 2) OnDiceRolled?.Invoke(diceRoll);
        else OnEndDiceRolls?.Invoke(new Vector2Int(_diceRolls[0], _diceRolls[1]));
    }
}
