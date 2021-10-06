using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuardArrest : MonoBehaviour
{
    [SerializeField] private float switchSceneDelay = 3;
    [SerializeField] private ScreenBlackFade screenBlackFade;
    
    private bool playerFound = false;

    public void Arrest()
    {
        if (playerFound)
            return; 
        
        playerFound = true;
        screenBlackFade.StartFade();

        StartCoroutine(SwitchSceneOnDelay());
    }

    IEnumerator SwitchSceneOnDelay()
    {
        yield return new WaitForSeconds(switchSceneDelay);
        
        RealityAnchor.currentScene = 0;

        if (SceneManager.GetActiveScene().buildIndex == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
            SceneManager.LoadScene(0);
    }
}