using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(INameAnimation))]
public class NameLogic : BaseLogic<INameAnimation>
{
    [Header("NameLogic References")]
    public Button SubmitButton;
    public TMP_InputField NameInput;

    protected override void SetupLogic()
    {
        SubmitButton.onClick.AddListener(() => _uiManager.EndName(NameInput.text));
        _uiManager.OnEndName += _ => NameInput.text = "";
    }

    protected override void SetupAnimation()
    {
        _uiManager.OnStartName += _animation.StartAnimation;
        _uiManager.OnEndName += _animation.StopAnimation;
    }
}