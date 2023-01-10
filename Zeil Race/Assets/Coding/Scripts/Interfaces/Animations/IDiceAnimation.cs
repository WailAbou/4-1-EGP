using System;

public interface IDiceAnimation
{
    public void MoveStartAnimation();
    public void MoveStopAnimation(int diceIndex, Action<int> onDiceStop);
    public void MoveEndAnimation(bool correct);
}