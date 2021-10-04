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

    public static int currentScene = 0;
    private float hue = 0;
    
    public void Hit()
    {
        StartCoroutine(SceneChange());
    }

    IEnumerator SceneChange()
    {
        //Do something
        
        yield return new WaitForSeconds(sceneChangeDelay);
        SceneManager.LoadScene(++currentScene);
    }

    private void Update()
    {
        _light.color = Color.HSVToRGB(hue + hueRate * Time.deltaTime, 1, 1);
    }
}
