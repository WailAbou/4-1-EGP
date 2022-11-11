using TMPro;
using UnityEngine;

[RequireComponent(typeof(IToastrAnimation))]
public class ToastrLogic : BaseLogic<IToastrAnimation>
{
    private GameObject _toastr;
    private RectTransform _toastrPanel;
    private TMP_Text _display;

    private void Awake()
    {
        _toastr = transform.GetChild(0).gameObject;
        _toastrPanel = _toastr.GetComponent<RectTransform>();
        _display = GetComponentInChildren<TMP_Text>(true);
    }

    protected override void SetupLogic()
    {
        _uiManager.OnStartToastr += StartToastr;
    }

    protected override void SetupAnimation()
    {
        _uiManager.OnStartToastr += _animation.StartToastrAnimation;
        _uiManager.OnEndToastr += _animation.EndToastrAnimation;
    }

    /// <summary>
    /// Starts the toastr by setting the text, setting the inital position and setting it active.
    /// </summary>
    /// <param name="text">The toastr text that needs to be displayed.</param>
    public void StartToastr(string text)
    {
        _display.text = text;
        _toastrPanel.anchoredPosition = new Vector3(0, -Screen.height / 2, 0);
        _toastrPanel.localScale = Vector3.zero;
        _toastr.SetActive(true);
    }
}
