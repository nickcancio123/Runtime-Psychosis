using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private GameObject _light;

    [SerializeField] private float maxInitialOnPeriod;
    [SerializeField] private float maxOnPeriod = 5f;
    [SerializeField] private float maxOffPeriod = 0.5f;
    [SerializeField] private bool isRandom = true;
    
    private void OnEnable()
    {
        StartCoroutine(InitialTurnOffDelay());
    }

    IEnumerator TurnOffDelay()
    {
        float delay = (isRandom) ? UnityEngine.Random.Range(0, maxOnPeriod) : maxOnPeriod;
        yield return new WaitForSeconds(delay);
        _light.SetActive(false);
        if (gameObject.activeInHierarchy)   
            StartCoroutine(TurnOnDelay());
    }

    IEnumerator TurnOnDelay()
    {
        float delay = (isRandom) ? UnityEngine.Random.Range(0, maxOffPeriod) : maxOffPeriod;
        yield return new WaitForSeconds(delay);
        _light.SetActive(true);
        if (gameObject.activeInHierarchy)
            StartCoroutine(TurnOffDelay());
    }
    
    IEnumerator InitialTurnOffDelay()
    {
        float delay = (isRandom) ? UnityEngine.Random.Range(0, maxInitialOnPeriod) : maxInitialOnPeriod;
        yield return new WaitForSeconds(delay);
        _light.SetActive(false);
        if (gameObject.activeInHierarchy)
            StartCoroutine(TurnOnDelay());
    }
}
