using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(INameAnimation))]
public class NameLogic : BaseLogic<INameAnimation>
{
    [Header("NameLogic References")]
    public Button SubmitButton;
    public TMP_InputField NameInput;

    protected override void SetupLogic()
    {
        SubmitButton.onClick.AddListener(SubmitName);
        NameInput.onSubmit.AddListener(_ => SubmitName());
    }

    protected override void SetupAnimation()
    {
        _uiManager.OnStartName += () => { _animation.StartAnimation(); NameInput.Select(); };
        _uiManager.OnEndName += _animation.StopAnimation;
    }

    private void SubmitName()
    {
        if (NameInput.text == "") return;

        _uiManager.EndName(NameInput.text);
        NameInput.text = "";
    }
}