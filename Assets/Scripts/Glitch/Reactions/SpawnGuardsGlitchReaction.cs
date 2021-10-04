using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnGuardsGlitchReaction : GlitchReaction
{
    [SerializeField] private List<GameObject> guards;
    [SerializeField] private List<GameObject> alarms;

    private void Start()
    {
        foreach (GameObject guard in guards)
            guard.SetActive(false);
    }

    public override void React()
    {
        foreach (GameObject guard in guards)
            guard.SetActive(true);
        
        foreach(GameObject alarm in alarms)
            alarm.GetComponent<Alarm>().SetOff();
    }
}
