using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Alarm : MonoBehaviour
{
    [SerializeField] private float flashPeriod = 0.5f;
    [SerializeField] private GameObject _light;
    private AudioSource audioSource;
    
    private bool flashing = false;
    private float lastFlashTime = 0;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (flashing)
        {
            if (Time.time - lastFlashTime > flashPeriod)
            {
                print("Flash");
                _light.SetActive(!_light.activeInHierarchy);
                lastFlashTime = Time.time;
            }
        }
    }

    public void SetOff()
    {
        print("Setoff");
        audioSource.Play();
        flashing = true;
    }
}
