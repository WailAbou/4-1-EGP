using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoordinatesAnimation : MonoBehaviour, ICoordinatesAnimation
{
    [Header("CoordinatesAnimation References")]
    public RectTransform CoordinatesPanel;
    public TMP_Text CoordinatesDisplay;

    public void SpawnAnimation()
    {
        CoordinatesPanel.DOAnchorPosY(100, Animations.COORDINATES_SPAWN_DURATION);
    }

    public void StopAnimation()
    {
        CoordinatesPanel.DOAnchorPosY(-350, Animations.COORDINATES_END_DURATION);
    }
}
