using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GuardCharge : StateMachineBehaviour
{
    private GuardController guardController;
    private GuardChargeTrigger chargeTrigger;
    private PlayerController playerController;
    
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Charging");
        
        guardController = animator.gameObject.GetComponent<GuardController>();
        playerController = guardController.playerController;
       
        chargeTrigger = guardController.chargeTrigger;
        chargeTrigger.isCharging = true;

        guardController.rb.velocity = new Vector2(guardController.walkSpeed * guardController.faceDirection, 0);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chargeTrigger.isCharging = false;
        guardController.rb.velocity = Vector2.zero;
    }
}
