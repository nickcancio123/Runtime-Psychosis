using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hide : MovementModifier
{
    public bool canHide = false;
    private Vector3 initialScale;
    
    protected override void ReadInput()
    {
        playerInput.Interact.Hide.performed += ctx => HidePressed();
        playerInput.Interact.Hide.canceled += ctx => HideReleased();
    }

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void HidePressed()
    {
        
    }

    private void HideReleased()
    {
        
    }
    
}
