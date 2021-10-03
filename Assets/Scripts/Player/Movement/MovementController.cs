using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GroundContact groundContact;
    public Animator animator;
    
    private bool canMove = true;
    public bool CanMove() => canMove;
    public void DisableMovement() => canMove = false;

    private List<MovementModifier> modifiers = new List<MovementModifier>();
    public void AddModifier(MovementModifier modifier) => modifiers.Add(modifier);
    
    private int faceDirection;
    private Vector3 initialScale;

    private void Start()
    {
        canMove = true;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (canMove)
        {
            foreach(MovementModifier mod in modifiers)
                mod.Move();
        }
        
        SetFaceDirection();
    }

    void SetFaceDirection()
    {
        faceDirection = (int)Mathf.Sign(rb.velocity.x);
        transform.localScale = new Vector3(initialScale.x * faceDirection, initialScale.y, initialScale.z);
    }
    
}
