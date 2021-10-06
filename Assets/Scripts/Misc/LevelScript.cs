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
        screenBlackFade.StartFade();
    }
}
