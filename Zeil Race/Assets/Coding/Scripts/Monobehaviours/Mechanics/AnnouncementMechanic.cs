using TMPro;
using UnityEngine;

public class AnnouncementMechanic : MonoBehaviour
{
    private GameObject _announcementObj;
    private RectTransform _announcement;
    private TMP_Text _display;
    private UiManager _uiManager;

    private void Awake()
    {
        _announcementObj = transform.GetChild(0).gameObject;
        _announcement = _announcementObj.GetComponent<RectTransform>();
        _display = GetComponentInChildren<TMP_Text>(true);
    }

    private void Start()
    {
        _uiManager = UiManager.Instance;
        _uiManager.OnAnnouncementStart += AnnouncementStart;
        _uiManager.OnAnnouncementStop += AnnouncementStop;
    }

    public void AnnouncementStart(string text, float displayDuration)
    {
        _display.text = text;
        _announcement.anchoredPosition = new Vector3(0, -Screen.height / 2, 0);
        _announcementObj.SetActive(true);
    }

    public void AnnouncementStop()
    {
        _announcementObj.SetActive(false);
    }
}
