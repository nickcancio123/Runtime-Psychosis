using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class RealityAnchor : MonoBehaviour
{
    [SerializeField] private float sceneChangeDelay = 1;
    [SerializeField] private Light2D _light;
    [SerializeField] private float hueRate = 60;
    [SerializeField] private ParticleSystem binaryParticles;

    public static int currentScene = 0;
    private float hue = 0;
    
    public void Hit()
    {
        binaryParticles.Play();
        _light.intensity *= 2;
        _light.pointLightOuterRadius *= 2;
        
        StartCoroutine(SceneChange());
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(sceneChangeDelay);
        SceneManager.LoadScene(++currentScene);
    }

    private void Update()
    {
        _light.color = Color.HSVToRGB(hue + hueRate * Time.deltaTime, 1, 1);
    }
}
