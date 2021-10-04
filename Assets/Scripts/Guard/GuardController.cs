using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform bound1;
    public Transform bound2;
    public GuardChargeTrigger chargeTrigger;

    public bool patrol = true;
    public float patrolSpeed = 0;
    public float chargeSpeed = 1;
    
    public float maxPauseTime = 3;
    public float walkDirection = 1;
    [HideInInspector] public int faceDirection = 1;

    private Vector3 initialScale;
    [SerializeField] private Animator animator;

    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public bool isDead = false;


    public float sightDistance = 10;
    public static float eyeLevel = 0.7f;
    public LayerMask rayCastLayerMask;
    public bool doScan = true;
    
    private void Start()
    {
        initialScale = transform.localScale;
        ReleaseBounds();
    }

    private void ReleaseBounds()
    {
        bound1.transform.parent = null;
        bound2.transform.parent = null;
    }

    private void Update()
    {
        SetFaceDirection();
    }

    private void SetFaceDirection()
    {
        if (rb.velocity.x > 0)
            faceDirection = 1;
        if (rb.velocity.x < 0)
            faceDirection = -1;
        transform.localScale = new Vector3(initialScale.x * faceDirection, initialScale.y, initialScale.z);
    }

    public void OnDie()
    {
        isDead = true;
        animator.SetBool("Dead", true);
        rb.velocity = Vector2.zero;
    }
}