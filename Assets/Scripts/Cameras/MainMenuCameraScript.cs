using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuCameraScript : MonoBehaviour
{
    [Header("Pan 1")]
    [SerializeField] private int panStartSize = 1;
    [SerializeField] private float panfinalSize = 4;
    [SerializeField] private float panDuration = 10;
    
    private CinemachineVirtualCamera vcam;
    private float pan1StartTime = 0;

    private bool donePanning = false;
    public bool DonePanning() => donePanning;

    private void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.m_Lens.OrthographicSize = panStartSize;
        pan1StartTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - pan1StartTime < panDuration)
            vcam.m_Lens.OrthographicSize += ((panfinalSize - panStartSize) / panDuration) * Time.deltaTime;
        else
            donePanning = true;
    }
}
