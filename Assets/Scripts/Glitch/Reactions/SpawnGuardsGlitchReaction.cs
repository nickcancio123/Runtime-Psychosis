using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnGuards : GlitchReaction
{
    [SerializeField] private List<GameObject> guards;

    private void Start()
    {
        foreach (GameObject guard in guards)
            guard.SetActive(false);
    }

    public override void React()
    {
        foreach (GameObject guard in guards)
            guard.SetActive(true);
    }
}
