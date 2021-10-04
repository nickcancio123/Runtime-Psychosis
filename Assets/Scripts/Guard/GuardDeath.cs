using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GuardDeath : StateMachineBehaviour
{
    private GameObject guardObj;
    private GuardController guardController;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        guardObj = animator.gameObject;
        guardController = guardObj.GetComponent<GuardController>();
        guardController.StartCoroutine(FreezePeriod());
    }

    IEnumerator FreezePeriod()
    {
        yield return new WaitForSeconds(GuardController.deathFreezeDuration);
        
        guardController.StartCoroutine(KnockBackPeriod());
    }
    
    IEnumerator KnockBackPeriod()
    {
        guardObj.GetComponent<Rigidbody2D>().velocity =
            new Vector2(guardController.faceDirection * GuardController.deathKnockBackSpeed, 0);
        
        yield return new WaitForSeconds(GuardController.deathKnockBackDuration);
        
        guardObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        guardObj.GetComponent<Collider2D>().enabled = false;
    }
    
    
}
