using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaurdPausePatrol : StateMachineBehaviour
{
    private Animator _animator;
    
    private GameObject guardObj;
    private GuardController guardController;    
    private FlashLight flashLight;
    private Rigidbody2D rb;

    private float pauseDuration = 1;
    private float pauseStartTime = 0;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _animator = animator;
        guardObj = animator.gameObject;
        flashLight = guardObj.GetComponent<FlashLight>();
        guardController = guardObj.GetComponent<GuardController>();
        
        pauseDuration = Random.Range(0, guardController.maxPauseTime);
        pauseStartTime = Time.time;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!guardController.patrol)   
        {
            if (guardController.doScan && flashLight.isWorking)
                ScanForPlayer();
            return;
        }
        
        if (Time.time - pauseStartTime < pauseDuration) //If still pausing
        {
            if (guardController.doScan && flashLight.isWorking)
                ScanForPlayer();
        }
        else
        {
            _animator.SetBool("Pausing", false);
        }
    }

    private void ScanForPlayer()
    {
        Vector2 origin = guardObj.transform.position + new Vector3(1.1f * guardController.walkDirection, GuardController.eyeLevel);
        RaycastHit2D raycastHit = Physics2D.Raycast(origin, Vector2.right * guardController.walkDirection, guardController.sightDistance, guardController.rayCastLayerMask);
        Debug.DrawLine(origin, origin + Vector2.right * guardController.walkDirection * guardController.sightDistance, Color.green, Time.deltaTime);

        if (!raycastHit)
            return;

        if (raycastHit.collider.gameObject.CompareTag("Player"))
        {
            GameObject player = raycastHit.collider.gameObject;
            Hide hide = player.GetComponent<Hide>();
            if (hide.IsHiding())
                return;
            
            PlayerController playerController = player.GetComponent<PlayerController>();
            guardController.playerController = playerController;
            playerController.Spotted();
            
            _animator.SetTrigger("Spotted");
        }
    }
}
