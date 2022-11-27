using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ICoordinatesAnimation))]
public class CoordinatesLogic : BaseLogic<ICoordinatesAnimation>
{
    [Header("CoordinatesAnimation References")]
    public Button CoordinatesButton;

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
        var correct = true;
        _uiManager.EndCoordinates(correct);
    }
}