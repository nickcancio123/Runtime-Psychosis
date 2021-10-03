using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadButt : MovementModifier
{
    [SerializeField] private GameObject hbTrigger;
    [SerializeField] private float lungeSpeed = 18;
    [SerializeField] private float lungeDuration = 0.5f;
    [SerializeField] private float lungeDelay = 0.2f;

    protected override void ReadInput()
    {
        playerInput.Interact.HeadButt.performed += ctx => StartCoroutine(LungeDelay());
    }

    IEnumerator LungeDelay()
    {
        moveController.DisableOtherModifiers(this);
        yield return new WaitForSeconds(lungeDelay);
        Lunge();
    }

    private void Lunge()
    {
        if (!moveController.CanMove())
            return;

        hbTrigger.SetActive(true);
        
        Vector2 vel = moveController.rb.velocity;
        moveController.rb.velocity = new Vector2(lungeSpeed * moveController.faceDirection, vel.y);

        StartCoroutine(LungeEnd());
    }

    IEnumerator LungeEnd()
    {
        yield return new WaitForSeconds(lungeDuration);
        moveController.EnableOtherModifiers();   
        hbTrigger.SetActive(false);
    }
}
