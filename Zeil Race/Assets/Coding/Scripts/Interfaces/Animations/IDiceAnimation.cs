public interface IDiceAnimation
{
    public void MoveStartAnimation();
    public int MoveStopAnimation(int diceIndex);
    public void MoveEndAnimation(GridCell gridCell);
}