using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchInteraction : MonoBehaviour
{
    [SerializeField] private GameObject glitchBody;
    [SerializeField] private bool triggerOnStart = false;
    [SerializeField] private List<GlitchTrigger> triggers;
    [SerializeField] private List<GlitchReaction> reactions;

    private bool triggered = false;

    private void Start()
    {
        if (triggerOnStart)
            ActivateGlitch();
        else
            glitchBody.SetActive(false);
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
    }

    public void PlayReactions()
    {
        if (!triggered)
            return;
        foreach (GlitchReaction reaction in reactions)
            reaction.React();
    }
    
    
    
}
