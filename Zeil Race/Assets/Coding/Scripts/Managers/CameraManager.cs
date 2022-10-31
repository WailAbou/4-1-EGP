using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("CameraManager References")]
    public List<CinemachineVirtualCamera> Cams;
    public CinemachineVirtualCamera FollowCam;

    private GameManager _gameManager;
    private CinemachineVirtualCamera _camCurrent;
    private int _camIndex;

    public override void Start()
    {
        base.Start();
        _gameManager = GameManager.Instance;
        if (FollowCam) Cams.Add(FollowCam);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
            SwitchCams();

        if (FollowCam != null && _gameManager.CurrentPlayer != null)
        {
            FollowCam.Follow = _gameManager.CurrentPlayer.transform;
            FollowCam.LookAt = _gameManager.CurrentPlayer.transform;
        }
    }

    public void SwitchCams()
    {
        if (Cams?.Count > 1)
        {
            if (_camCurrent) _camCurrent.Priority = 10;
            _camCurrent = Cams[_camIndex];
            _camCurrent.Priority = 11;
            _camIndex = (_camIndex + 1) % Cams.Count;
        }
    }
}
