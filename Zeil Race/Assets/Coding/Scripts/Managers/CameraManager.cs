using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("CameraManager References")]
    public List<CinemachineVirtualCamera> Cams;
    public CinemachineVirtualCamera FollowCam;

    private CinemachineVirtualCamera _camCurrent;
    private int _camIndex;

    public override void Setup()
    {
        if (FollowCam)
        {
            _playerManager.OnPlayersSpawned += SetFollowCam;
            _playerManager.OnMoveEnd += SetFollowTarget;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
            SwitchCams();
    }

    private void SetFollowCam(PlayerLogic[] players)
    {
        Cams.Add(FollowCam);
        SetFollowTarget(players[0].transform);
    }

    private void SetFollowTarget(Transform player)
    {
        FollowCam.Follow = player.transform;
        FollowCam.LookAt = player.transform;
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
