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
    [SerializeField] private float lowVolume = 10;
    [SerializeField] private float highVolume = 30;
    [SerializeField] private ParticleSystem destructionParticles;
    
    private List<GlitchTrigger> triggers = new List<GlitchTrigger>();
    private List<GlitchReaction> reactions = new List<GlitchReaction>();
    private AudioSource audioSource;

    private bool triggered = false;

    private void Start()
    {
        GetTriggersAndReactions();
        
        if (triggerOnStart)
            ActivateGlitch();
        else
            glitchBody.SetActive(false);
        
        audioSource = gameObject.GetComponent<AudioSource>();
        if (!audioSource)
            return;
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
        
        audioSource = gameObject.GetComponent<AudioSource>();
        if (!audioSource)
            return;
        audioSource.volume = highVolume;
    }

    public void PlayReactions()
    {
        if (!triggered)
            return;
        foreach (GlitchReaction reaction in reactions)
        {
            reaction.React();
        }

        destructionParticles.Play();
        destructionParticles.transform.parent = null;
        Destroy(this.gameObject);
    }

    private void GetTriggersAndReactions()
    {
        GlitchTrigger[] _triggers = gameObject.GetComponents<GlitchTrigger>();
        
        foreach (GlitchTrigger _trigger in _triggers)
        {
            triggers.Add(_trigger);
        }
        
        GlitchReaction[] _reactions = gameObject.GetComponents<GlitchReaction>();
        foreach (GlitchReaction _reaction in _reactions)
        {
            reactions.Add(_reaction);
        }
    }
    
    IEnumerator SelfDestruct(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
