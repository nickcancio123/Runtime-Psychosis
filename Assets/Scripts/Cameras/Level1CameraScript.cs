using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Level1CameraScript : MonoBehaviour
{
    [Header("Pan 1")]
    [SerializeField] private int pan1StartSize = 1;
    [SerializeField] private float pan1finalSize = 4;
    [SerializeField] private float pan1Duration = 10;
    [Header("Pan 2")]
    [SerializeField] private float pan2finalSize = 6;
    [SerializeField] private float pan2Duration = 2;
    [SerializeField] private Vector2 pan2FollowOffset;
    [SerializeField] private PolygonCollider2D level1Confiner;
    [SerializeField] private PolygonCollider2D mainMenuConfiner;
    
    public bool inMainMenu = true; 
    
    private CinemachineVirtualCamera vcam;
    private CinemachineConfiner2D confiner;
    private float pan1StartTime = 0;
    
    private bool startedPan2 = false;
    private float pan2StartTime = 0;

    private void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner2D>();

        confiner.m_BoundingShape2D = mainMenuConfiner;
        vcam.m_Lens.OrthographicSize = pan1StartSize;
        pan1StartTime = Time.time;
    }

    private void Update()
    {
        //If in main menu and not done panning out
        if (inMainMenu)
        {
            if (Time.time - pan1StartTime < pan1Duration)
                vcam.m_Lens.OrthographicSize +=  ((pan1finalSize - pan1StartSize) / pan1Duration) * Time.deltaTime;
        }
        //Start pan 2
        else if (!startedPan2)
        {
            startedPan2 = true;
            pan2StartTime = Time.time;
        }
        //Switch confiner and adjust size and offset
        else if (Time.time - pan2StartTime < pan2Duration)
        {
            confiner.m_BoundingShape2D = level1Confiner;
            vcam.m_Lens.OrthographicSize +=  ((pan2finalSize - pan1finalSize) / pan2Duration) * Time.deltaTime;
        }
    }
}
