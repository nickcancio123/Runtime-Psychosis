using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GuardPatrol : StateMachineBehaviour
{
    private Animator _animator;
    
    private GameObject guardObj;
    private GuardController guardController;
    private FlashLight flashLight;
    private Rigidbody2D rb;

    private Transform bound1;
    private Transform bound2;

    private Transform leftBound;
    private Transform rightBound;

    private bool touchingPlayer;

    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        
        guardController.leftFootAudio.Play();
        guardController.StartCoroutine(HalfFootStepDelay());
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!guardController.patrol)
        {
            ReachedEnd();
            return;
        }
        
        if (guardController.doScan && flashLight.isWorking)
            ScanForPlayer();

        //If in bounds, walk
        if ((guardController.walkDirection < 0 && guardObj.transform.position.x > leftBound.position.x) ||
            (guardController.walkDirection > 0 && guardObj.transform.position.x < rightBound.position.x))
        {
            rb.velocity = new Vector2(guardController.walkDirection * guardController.patrolSpeed, rb.velocity.y);
        }
        //Else pause
        else
            ReachedEnd();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        guardController.leftFootAudio.Stop();
        guardController.rightFootAudio.Stop();
    }

    private void Initialize(Animator animator)
    {
        _animator = animator;
        guardObj = animator.gameObject;
        flashLight = guardObj.GetComponent<FlashLight>();
        guardController = guardObj.GetComponent<GuardController>();
        rb = guardController.rb;

        bound1 = guardController.bound1;
        bound2 = guardController.bound2;
        
        leftBound = (bound1.position.x < bound2.position.x) ? bound1 : bound2;
        rightBound = (bound1.position.x > bound2.position.x) ? bound1 : bound2;
        
        SwitchDirection();
    }
    
    private void SwitchDirection() => guardController.walkDirection *= -1;

    private void ReachedEnd()
    {
        rb.velocity = Vector2.zero;
        _animator.SetBool("Pausing", true);
    }

    private void ScanForPlayer()
    {
        Vector2 origin = guardObj.transform.position + new Vector3(1.1f * guardController.faceDirection, GuardController.eyeLevel);
        RaycastHit2D raycastHit = Physics2D.Raycast(origin, Vector2.right * guardController.faceDirection, guardController.sightDistance, guardController.rayCastLayerMask);
        Debug.DrawLine(origin, origin + Vector2.right * guardController.faceDirection * guardController.sightDistance, Color.green, Time.deltaTime);

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
            
            guardObj.GetComponent<GuardArrest>().Arrest();

            _animator.SetTrigger("Spotted");
        }
    }
    
    IEnumerator HalfFootStepDelay()
    {
        yield return new WaitForSeconds(guardController.leftFootAudio.clip.length / 2);
        guardController.rightFootAudio.Play();
    }
}

