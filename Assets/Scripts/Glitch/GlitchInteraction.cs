using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class GlitchInteraction : MonoBehaviour
{
    [SerializeField] private GameObject glitchBody;
    [SerializeField] private bool triggerOnStart = false;
    [SerializeField] private List<GlitchTrigger> triggers;
    [SerializeField] private List<GlitchReaction> reactions;
    [SerializeField] private float lowVolume = 10;
    [SerializeField] private float highVolume = 30;
    [SerializeField] private ParticleSystem destructionParticles;
    private AudioSource audioSource;

    private bool triggered = false;

    private void Start()
    {
        if (triggerOnStart)
            ActivateGlitch();
        else
            glitchBody.SetActive(false);

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = lowVolume;
        audioSource.Play();
    }

    private void Update()
    {
        if (!triggered)
            CheckTriggers();
    }

    private void CheckTriggers()
    {
        bool allTriggered = true;
        foreach (GlitchTrigger trigger in triggers)
        {
            if (!trigger.IsTriggered())
            {
                allTriggered = false;
                break;
            }
        }

        if (allTriggered) 
            ActivateGlitch();
    }

    private void ActivateGlitch()
    {
        triggered = true;
        glitchBody.SetActive(true);
        audioSource.volume = highVolume;
    }

    public void PlayReactions()
    {
        if (!triggered)
            return;
        foreach (GlitchReaction reaction in reactions)
            reaction.React();

        //destructionParticles.Play();
        StartCoroutine(SelfDestruct(destructionParticles.main.duration));
    }

    IEnumerator SelfDestruct(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
