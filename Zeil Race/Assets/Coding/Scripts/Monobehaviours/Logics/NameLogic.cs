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
        SubmitButton.onClick.AddListener(() => SubmitName());
    }

    protected override void SetupAnimation()
    {
        _uiManager.OnStartName += _animation.StartAnimation;
        _uiManager.OnEndName += _animation.StopAnimation;
    }

    private void SubmitName()
    {
        if (NameInput.text == "") return;

        _uiManager.EndName(NameInput.text);
        NameInput.text = "";
    }
}