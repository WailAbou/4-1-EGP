using System;

public interface IDiceAnimation
{
    public void MoveStartAnimation(int allowedRolls);
    public void MoveStopAnimation(int diceIndex, Action<int> onDiceStop);
    public void MoveEndAnimation(CellLogic cell, int allowedRolls);
}