using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private GameObject lightEffect;
    public bool isWorking = true;

    private void Update()
    {
        ShineLight();
    }

    private void ShineLight()
    {
        if (isWorking)
            lightEffect.SetActive(true);
        else
            lightEffect.SetActive(false);
    }
}
