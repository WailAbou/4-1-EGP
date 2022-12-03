using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoordinatesAnimation : MonoBehaviour, ICoordinatesAnimation
{
    [Header("CoordinatesAnimation References")]
    public RectTransform CoordinatesPanel;
    public TMP_Text CoordinatesDisplay;

    public void SpawnAnimation(string text)
    {
        CoordinatesPanel.DOScale(Vector3.one, Animations.COORDINATES_SPAWN_DURATION);
        CoordinatesDisplay.SetText(text);
    }

    public void StopAnimation()
    {
        CoordinatesPanel.DOScale(Vector3.zero, Animations.COORDINATES_END_DURATION);
    }
}
