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

    private Color initialColor;
    private Vector3 initialScale;
    
    protected override void ReadInput()
    {
        playerInput.Interact.Hide.performed += ctx => HidePressed();
        playerInput.Interact.Hide.canceled += ctx => HideReleased();
    }

    private void Start()
    {
        initialScale = transform.localScale;
        initialColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void HidePressed()
    {
        if (!moveController.CanMove())
            return;

        if (!canHide)
            return;

        isHiding = true;
        moveController.animator.SetBool("Crouching", true);
        moveController.DisableOtherModifiers(this);
        
        //temp
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void HideReleased()
    {
        if (!moveController.CanMove())
            return;

        if (!isHiding)
            return;

        isHiding = false;
        moveController.animator.SetBool("Crouching", false);
        moveController.EnableOtherModifiers();
        
        
        //temp
        gameObject.GetComponent<SpriteRenderer>().color = initialColor;
    }
}
