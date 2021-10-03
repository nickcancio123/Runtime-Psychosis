using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform bound1;
    public Transform bound2;

    public bool doWalk = true;
    public bool doScan = true;
    
    public float walkSpeed = 0;
    public float maxPauseTime = 3;
    public float sightDistance = 10;
    public LayerMask rayCastLayerMask;

    public static float eyeLevel = 0.7f;

    private void Start()
    {
        ReleaseBounds();
    }

    private void ReleaseBounds()
    {
        bound1.transform.parent = null;
        bound2.transform.parent = null;
    }
}