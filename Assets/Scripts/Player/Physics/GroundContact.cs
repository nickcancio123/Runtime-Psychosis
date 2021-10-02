using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundContact : MonoBehaviour
{
    private bool isGrounded = false;
    public bool IsGrounded() => isGrounded;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
