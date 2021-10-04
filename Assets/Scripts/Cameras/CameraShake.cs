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
    
    public void Shake()
    {
        //Get virtual camera in control of main camera
        CinemachineVirtualCamera[] vCams = FindObjectsOfType<CinemachineVirtualCamera>();
        int highestPriority = 0;
        vCam = vCams[0];
        foreach (var cam in vCams)
        {
            if (cam.Priority > highestPriority)
            {
                highestPriority = cam.Priority;
                vCam = cam;
            }
        }
        
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
