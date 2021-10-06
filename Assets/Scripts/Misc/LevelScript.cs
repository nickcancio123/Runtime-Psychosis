using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private ScreenBlackFade screenBlackFade;

    private void Start()
    {
        Cursor.visible = false;
        screenBlackFade.StartFade();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
