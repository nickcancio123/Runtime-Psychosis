using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private GameObject lightEffect;
    [SerializeField] private GuardController guardController;
    public bool isWorking = true;

    private void Update()
    {
        ShineLight();
    }

    private void ShineLight()
    {
        if (isWorking && guardController.doScan)
            lightEffect.SetActive(true);
        else
            lightEffect.SetActive(false);
    }
}
