using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using System;

public class CameraManager : Singleton<CameraManager>
{
    [Header("CameraManager References")]
    public List<CinemachineVirtualCamera> Cams;
    public CinemachineVirtualCamera FollowCam;
    public Action<int> OnSwitchCam;

    private CinemachineVirtualCamera _camCurrent;
    private int _camIndex;

    public override void Setup()
    {
        _camCurrent = Cams[_camIndex];
        _camCurrent.Priority = 11;

        _playerManager.OnPlayersSpawned += SetFollowCam;
        _playerManager.OnTurnStart += SetFollowTarget;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
            SwitchCams();
    }

    private void SetFollowCam(PlayerLogic[] players)
    {
        Cams.Add(FollowCam);
        SetFollowTarget(players[0].transform, Vector2Int.zero);
    }

    private void SetFollowTarget(Transform player, Vector2Int gridPosition)
    {
        FollowCam.Follow = player.transform;
        FollowCam.LookAt = player.transform;
    }

    public void SwitchCams()
    {
        if (_camCurrent) _camCurrent.Priority = 10;
        _camIndex = (_camIndex + 1) % Cams.Count;

        _camCurrent = Cams[_camIndex];
        _camCurrent.Priority = 11;

        OnSwitchCam?.Invoke(_camIndex);
    }
}
