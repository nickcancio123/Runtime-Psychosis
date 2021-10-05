using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGuardDirectionGlitchReaction : GlitchReaction
{
    [SerializeField] private List<GameObject> guards;
    public override void React()
    {
        foreach (GameObject guard in guards)
        {
            guard.GetComponent<GuardController>().faceDirection *= -1;
        }
    }
}
