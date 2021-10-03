using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardChargeTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [HideInInspector] public bool isCharging = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCharging)
            return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            other.gameObject.GetComponent<PlayerController>().GotCaught();
            animator.SetTrigger("CaughtPlayer");
        }
    }
}
