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
        CoordinatesPanel.DOScale(Vector3.one, 1.0f);
        CoordinatesDisplay.SetText(text);
    }
}
