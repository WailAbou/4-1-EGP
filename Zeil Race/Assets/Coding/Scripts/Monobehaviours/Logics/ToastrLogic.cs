using TMPro;
using UnityEngine;

[RequireComponent(typeof(IToastrAnimation))]
public class ToastrLogic : BaseLogic<IToastrAnimation>
{
    [Header("ToastrLogic References")]
    public RectTransform ToastrPanel;
    public TMP_Text TitleDisplay;
    public TMP_Text SubtitleDisplay;

    protected override void SetupLogic()
    {
        _uiManager.OnStartToastr += StartToastr;
    }

    protected override void SetupAnimation()
    {
        _uiManager.OnStartToastr += _animation.StartToastrAnimation;
    }

    public void StartToastr(string title, string subtitle)
    {
        TitleDisplay.text = title;
        SubtitleDisplay.text = subtitle;

        ToastrPanel.anchoredPosition = new Vector3(0, -Screen.height / 2, 0);
        ToastrPanel.localScale = Vector3.zero;
        ToastrPanel.gameObject.SetActive(true);
    }
}
