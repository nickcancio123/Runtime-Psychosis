using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadButtTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip headButtHit;
    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] private CameraShake camShake;

    private void OnTriggerEnter2D(Collider2D other) => OnTriggerDetection(other);

    private void OnTriggerStay2D(Collider2D other) => OnTriggerDetection(other);

    private void OnTriggerDetection(Collider2D other)
    {
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
    }

    private void OnHeadButtHitBase()
    {
        playerAudioSource.clip = headButtHit;
        playerAudioSource.Play();
        camShake.Shake(null);
    }
}
