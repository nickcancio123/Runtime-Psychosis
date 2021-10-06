using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cinemachine;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform bound1;
    public Transform bound2;
    public GuardChargeTrigger chargeTrigger;
    public GameObject glitchHead;
    public ScreenBlackFade screenBlackFade;
    public FlashLight flashLight;

    public bool patrol = true;
    public float patrolSpeed = 0;
    public float chargeSpeed = 1;
    
    public float maxPauseTime = 3;
    [HideInInspector] public float walkDirection = 1;
    public int faceDirection = 1;

    private Vector3 initialScale;
    [SerializeField] private Animator animator;

    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public bool isDead = false;


    public float sightDistance = 10;
    public LayerMask rayCastLayerMask;
    public bool doScan = true;
    public static float eyeLevel = 0.7f;
    public static float deathFreezeDuration = 0.2f;
    public static float deathKnockBackSpeed = 20;
    public static float deathKnockBackDuration = 0.3f;

    public AudioSource leftFootAudio;
    public AudioSource rightFootAudio;

    [SerializeField] private bool isJojo = false;
    
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
        
        //If player hiding, don't collide
        GameObject player = GameObject.Find("Player");
        if (!player)
            return;
        if (isDead)
            return;
        if (player.GetComponent<Hide>().IsHiding())
            gameObject.GetComponent<Collider2D>().enabled = false;
        else
            gameObject.GetComponent<Collider2D>().enabled = true;
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
        if (isJojo)
        {
            Destroy(gameObject);
            return;
        }
        
        isDead = true;
        animator.SetBool("Dead", true);
        glitchHead.SetActive(false);
        rb.velocity = Vector2.zero;
    }
}