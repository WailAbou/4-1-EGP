using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ICoordinatesAnimation))]
public class CoordinatesLogic : BaseLogic<ICoordinatesAnimation>
{
    [Header("CoordinatesAnimation References")]
    public Button CoordinatesButton;
    public TMP_InputField XCoordinates;
    public TMP_InputField YCoordinates;

    protected override void SetupLogic()
    {
        CoordinatesButton.onClick.AddListener(() => CheckAnswer());
    }

    protected override void SetupAnimation()
    {
        _uiManager.OnStartCoordinates += _animation.SpawnAnimation;
    }

    private void CheckAnswer()
    {
        _animation.StopAnimation();
        var coordinates = new Vector2Int(int.Parse(XCoordinates.text), int.Parse(YCoordinates.text));
        _uiManager.EndCoordinates(coordinates);
    }
}