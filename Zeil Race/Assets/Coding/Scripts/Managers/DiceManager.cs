using System;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : Singleton<DiceManager>
{
    public Action<int> OnStartDiceRolls;
    public Action<int> OnDiceRolled;
    public Action<int> OnEndDiceRolls;

    private List<Transform> _extraRollPlayers = new List<Transform>();
    private int _allowedRolls = 1;
    private int _diceRoll;
    private int _diceIndex;

    public override void Setup()
    {
        _playerManager.OnTurnStart += StartDiceRolls;
    }

    public void StartDiceRolls(Transform player, Vector2Int gridPosition)
    {
        _allowedRolls = _extraRollPlayers.Contains(player) ? 2 : 1;
        _extraRollPlayers.Remove(player);

        _diceRoll = 0;
        _diceIndex = 0;
        OnStartDiceRolls?.Invoke(_allowedRolls);
    }

    public void ExtraRoll(PlayerLogic player) 
    {
        _extraRollPlayers.Add(player.transform);
    }

    public void EndRollDices(int diceRoll)
    {
        _diceRoll += diceRoll;
        _diceIndex++;

        if (_diceIndex < _allowedRolls) OnDiceRolled?.Invoke(_diceIndex);
        else
        {
            _allowedRolls = 1;
            OnEndDiceRolls?.Invoke(_diceRoll);
        }
    }
}
