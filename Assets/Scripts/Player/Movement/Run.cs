using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MovementModifier
{
    [SerializeField] private float runSpeed = 10;
    [SerializeField] private float groundAccel = 0.4f;
    [SerializeField] private float airAccel = 0.2f;
    
    private float runInput = 0;
    
    protected override void ReadInput()
    {
        playerInput.Movement.Run.performed += ctx => runInput = ctx.ReadValue<float>();
        playerInput.Movement.Run.canceled += ctx => runInput = 0;
    }

    public override void Move()
    {
        Rigidbody2D rb = moveController.rb;
        Vector2 targetVel = new Vector2(runInput * runSpeed, rb.velocity.y);
        float acceleration = (moveController.groundContact.IsGrounded()) ? groundAccel : airAccel;
        moveController.rb.velocity = Vector2.Lerp(rb.velocity, targetVel, acceleration);
    }
}
