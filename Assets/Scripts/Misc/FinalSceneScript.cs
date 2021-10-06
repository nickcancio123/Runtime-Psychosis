using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinalSceneScript : MonoBehaviour
{
    [SerializeField] private float delay = 10;
    
    private void Start()
    {
        StartCoroutine(DelayQuit());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    IEnumerator DelayQuit()
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
