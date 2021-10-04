using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private MovementController movement;

    
    private void Update()
    {
        UpdateAnimationParams();
    }

    private void UpdateAnimationParams()
    {
        animator.SetFloat("xVel", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVel", rb.velocity.y);
        animator.SetBool("IsGrounded", movement.groundContact.IsGrounded());
    }

    public void Spotted()
    {
        rb.velocity = Vector2.zero;
        movement.DisableMovement();
        animator.SetTrigger("Spotted");
    }

    public void GotCaught()
    {
        Camera.main.gameObject.transform.parent = null;
        Destroy(this.gameObject);
    }
}
