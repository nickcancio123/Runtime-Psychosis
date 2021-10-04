using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MovementModifier
{
    [SerializeField] private float jumpSpeed = 15;
    [SerializeField] private float jumpExtensionDuration = 0.5f;

    private bool isJumping = false;
    private float jumpStartTime = 0;

    protected override void ReadInput()
    {
        playerInput.Movement.Jump.performed += ctx => OnJumpPress();
        playerInput.Movement.Jump.canceled += ctx => OnJumpRelease();
    }

    private void OnJumpPress()
    {
        if (!moveController.CanMove())
            return;

        if (!moveController.groundContact.IsGrounded())
            return;

        isJumping = true;
        jumpStartTime = Time.time;
        moveController.animator.SetTrigger("Jump");
    }

    private void OnJumpRelease() => isJumping = false;

    public override void Move()
    {
        if (isJumping && Time.time - jumpStartTime < jumpExtensionDuration)
        {
            Rigidbody2D rb = moveController.rb;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
}
