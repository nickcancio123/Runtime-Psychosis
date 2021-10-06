using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadButt : MovementModifier
{
    [SerializeField] private GameObject hbTrigger;
    [SerializeField] private AudioClip whoosh;
    [SerializeField] private float lungeSpeed = 18;
    [SerializeField] private float lungeDuration = 0.5f;
    [SerializeField] private float lungeDelay = 0.2f;
    [SerializeField] private float coolDown = 2.0f;

    [HideInInspector] public bool isHeadButting = false;
    
    private AudioSource audioSource;
    private float hbStartTime = 0;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    protected override void ReadInput()
    {
        playerInput.Interact.HeadButt.performed += ctx => StartCoroutine(LungeDelay());
    }
    
    public IEnumerator LungeDelay()
    {
        if (!moveController.CanMove() || !moveController.enabled)
            yield break;

        if (!moveController.groundContact.IsGrounded())
            yield break;

        if (Time.time - hbStartTime < coolDown)
            yield break;
        
        hbStartTime = Time.time;

        isHeadButting = true;
        
        moveController.DisableOtherModifiers(this);
        moveController.animator.SetTrigger("HeadButt");
        
        yield return new WaitForSeconds(lungeDelay);
        
        Lunge();
    }

    private void Lunge()
    {
        hbTrigger.SetActive(true);
        
        Vector2 vel = moveController.rb.velocity;
        moveController.rb.velocity = new Vector2(lungeSpeed * moveController.faceDirection, vel.y);
        
        audioSource.clip = whoosh;
        audioSource.Play();

        StartCoroutine(LungeEnd());
    }

    IEnumerator LungeEnd()
    {
        yield return new WaitForSeconds(lungeDuration);
        moveController.EnableOtherModifiers();   
        hbTrigger.SetActive(false);

        isHeadButting = false;
    }
}
