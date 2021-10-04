using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float zoomInAmount = 0.5f;
    [SerializeField] private float zoomDuration = 0.1f;

    private CinemachineVirtualCamera vCam;
    
    public void Shake(CinemachineVirtualCamera _vCam)
    {
        if (_vCam)
            vCam = _vCam;
        else
            vCam = FindObjectOfType<CinemachineVirtualCamera>();
        
        if (vCam)
        {
            vCam.m_Lens.OrthographicSize -= zoomInAmount;
            StartCoroutine(Unshake());
        }
    }

    IEnumerator Unshake()
    {
        yield return new WaitForSeconds(zoomDuration);
        vCam.m_Lens.OrthographicSize += zoomInAmount;
    }
}
