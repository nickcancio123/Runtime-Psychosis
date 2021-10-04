using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGuardsGlitchTrigger : GlitchTrigger
{
    [SerializeField] private List<GameObject> guards;

    public override bool IsTriggered()
    {
        bool allDead = true;
        foreach (GameObject guard in guards)
        {
            GuardController guardController = guard.GetComponent<GuardController>();
            if (!guardController.isDead)
            {
                allDead = false;
                break;
            }
        }
        return allDead; 
    }
}
