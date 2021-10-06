using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBlackFade : MonoBehaviour
{
    [SerializeField] private bool fadeToBlack = true;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image blackImage;
    [SerializeField] private float fadeDuration = 2;
    
    private bool doFade = false;
    private float fadeStartTime = 0;

    private void Start()
    {
        blackImage.color = new Color(0, 0, 0, (fadeToBlack) ? 0 : 1);
    }

    public void StartFade()
    {
        doFade = true;
        fadeStartTime = Time.time;
        canvas.SetActive(true);
    }

    private void Update()
    {
        if (doFade)
        {
            if (Time.time - fadeStartTime < fadeDuration)
            {
                Color col = blackImage.color;
                float deltaAlphaSign = (fadeToBlack) ? 1 : -1;
                blackImage.color = new Color(0, 0, 0, col.a + deltaAlphaSign * Time.deltaTime / fadeDuration);
                
                if (!fadeToBlack)
                {
                    if (blackImage.color.a <= 0)
                    {
                        doFade = false;
                        canvas.SetActive(false);
                    }
                }
            }
        }
    }
}
