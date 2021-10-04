using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuardArrest : MonoBehaviour
{
    [SerializeField] private Image blackImage;
    [SerializeField] private float fadeDuration = 2;
    [SerializeField] private float fadeRate = 2;
    private bool isFading = false;
    private float fadeStartTime = 0;

    public void Arrest()
    {
        if (isFading)
            return; 
        
        isFading = true;
        fadeStartTime = Time.time;
    }

    private void Update()
    {
        if (isFading)
        {
            if (Time.time - fadeStartTime < fadeDuration)
            {
                Color col = blackImage.color;
                blackImage.color = new Color(0, 0, 0, col.a + Time.deltaTime * fadeRate);
            }
            else
            {
                RealityAnchor.currentScene = 0;
                
                if (SceneManager.GetActiveScene().buildIndex == 0)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                else
                    SceneManager.LoadScene(0);
            }
        }
    }
}