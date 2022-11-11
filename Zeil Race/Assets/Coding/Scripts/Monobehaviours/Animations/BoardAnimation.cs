using DG.Tweening;
using UnityEngine;

public class BoardAnimation : MonoBehaviour, IBoardAnimation
{
    public void SpawnAnimation()
    {
        transform.DOPunchScale(new Vector3(0.25f, 0, 0.25f), Animations.BOARD_SPAWN_DURATION);
    }
}
