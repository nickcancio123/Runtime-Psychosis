using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GroundContact groundContact;
    public Animator animator;
    
    private bool canMove = true;
    public bool CanMove() => canMove;
    public void DisableMovement() => canMove = false;

    [HideInInspector] public int faceDirection = 1;
    private Vector3 initialScale;
    
    
    private List<MovementModifier> modifiers = new List<MovementModifier>();
    public void AddModifier(MovementModifier modifier) => modifiers.Add(modifier);
    
    
    public void DisableOtherModifiers(MovementModifier caller)
    {
        foreach (MovementModifier mod in modifiers)
        {
            if (mod != caller)
                mod.active = false;
        }
    }
    
    public void EnableOtherModifiers()
    {
        foreach (MovementModifier mod in modifiers)
            mod.active = true;
    }

    
    
    private void Start()
    {
        canMove = true;
        initialScale = transform.localScale;
        EnableOtherModifiers();
    }

    private void Update()
    {
        if (canMove)
        {
            foreach (MovementModifier mod in modifiers)
            {
                if (mod.active)
                    mod.Move();
            }
        }
        
        SetFaceDirection();
    }

    void SetFaceDirection()
    {
        if (rb.velocity.x > 0)
            faceDirection = 1;
        if (rb.velocity.x < 0)
            faceDirection = -1;
        transform.localScale = new Vector3(initialScale.x * faceDirection, initialScale.y, initialScale.z);
    }
    
}
