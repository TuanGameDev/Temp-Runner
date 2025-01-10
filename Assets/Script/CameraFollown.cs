using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollown : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;

    private PlayerController _player;

    private void Start()
    {
        Invoke(nameof(GetCameraFollown), 0.1f);
    }
    void GetCameraFollown()
    {
        _player = GameObject.FindObjectOfType<PlayerController>();

        if (_player != null)
        {
            Transform pointCam = _player.transform.Find("PointCam");

            if (pointCam != null)
            {
                _virtualCamera.Follow = pointCam;
            }
        }
    }
}
