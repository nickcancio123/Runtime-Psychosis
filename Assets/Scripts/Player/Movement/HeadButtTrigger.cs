using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadButtTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnHeadButtObject(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnHeadButtObject(other);
    }

    private void OnHeadButtObject(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<GuardController>().OnDie();
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Glitch"))
        {
            other.gameObject.transform.parent.gameObject.GetComponent<GlitchInteraction>().PlayReactions();
            gameObject.SetActive(false);
        }
    }
}
