using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hide : MovementModifier
{
    [HideInInspector] public bool canHide = false;
    
    private bool isHiding = false;
    public bool IsHiding() => isHiding;

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
        if (!moveController.CanMove())
            return;

        if (!canHide)
            return;
        
        isHiding = true;
        moveController.rb.velocity = Vector2.zero;
        moveController.animator.SetBool("Hiding", true);
        moveController.DisableOtherModifiers(this);
    }

    private void HideReleased()
    {
        if (!moveController.CanMove())
            return;

        if (!isHiding)
            return;

        isHiding = false;
        moveController.animator.SetBool("Hiding", false);
        moveController.EnableOtherModifiers();
    }
}
