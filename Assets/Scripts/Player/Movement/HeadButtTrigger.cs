using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadButtTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip headButtHit;
    [SerializeField] private AudioClip anchorHitSound;
    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] private CameraShake camShake;

    private bool hit = false;

    private void OnTriggerEnter2D(Collider2D other) => OnTriggerDetection(other);

    private void OnTriggerStay2D(Collider2D other) => OnTriggerDetection(other);

    private void OnTriggerDetection(Collider2D other)
    {
        if (hit)
            return;
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<GuardController>().OnDie();
            OnHeadButtHitBase();
        }

        if (other.gameObject.CompareTag("Glitch"))
        {
            other.gameObject.transform.parent.gameObject.GetComponent<GlitchInteraction>().PlayReactions();
            OnHeadButtHitBase();
        }

        if (other.gameObject.CompareTag("RealityAnchor"))
        {
            other.gameObject.GetComponent<RealityAnchor>().Hit();
            HitRealityAnchor();
        }
    }

    private void OnDisable()
    {
        hit = false;
    }

    private void OnHeadButtHitBase()
    {
        playerAudioSource.clip = headButtHit;
        playerAudioSource.Play();
        camShake.Shake(null);
        hit = true;
    }

    private void HitRealityAnchor()
    {
        playerAudioSource.clip = anchorHitSound;
        playerAudioSource.Play();
        camShake.Shake(null);
        hit = true;
    }

    
}
