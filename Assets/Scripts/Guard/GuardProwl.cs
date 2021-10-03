using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuardProwl : StateMachineBehaviour
{
    private Animator _animator;
    
    private GameObject guardObj;
    private GuardController guardController;
    private Rigidbody2D rb;

    private Transform bound1;
    private Transform bound2;

    private Transform leftBound;
    private Transform rightBound;
    private bool doWalk = true;
    private bool doScan = true;
    private int walkDirection = -1;

    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (doScan)
            ScanForPlayer();

        if (!doWalk)
            return;
        
        //If in bounds, walk
        if ((walkDirection < 0 && guardObj.transform.position.x > leftBound.position.x) ||
            (walkDirection > 0 && guardObj.transform.position.x < rightBound.position.x))
        {
            rb.velocity = new Vector2(walkDirection * guardController.walkSpeed, rb.velocity.y);
        }
        //Else pause
        else
            ReachedEnd();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private void Initialize(Animator animator)
    {
        _animator = animator;
        guardObj = animator.gameObject;
        guardController = guardObj.GetComponent<GuardController>();
        rb = guardController.rb;
        walkDirection = (Random.Range(0, 2) > 0) ? -1 : 1;

        doWalk = guardController.doWalk;
        doScan = guardController.doScan;
        
        bound1 = guardController.bound1;
        bound2 = guardController.bound2;
        leftBound = (bound1.position.x < bound2.position.x) ? bound1 : bound2;
        rightBound = (bound1.position.x > bound2.position.x) ? bound1 : bound2;
    }
    
    private void SwitchDirection() => walkDirection *= -1;

    private void ReachedEnd()
    {
        doWalk = false;
        rb.velocity = Vector2.zero;
        float pauseDuration = Random.Range(0, guardController.maxPauseTime);
        guardController.StartCoroutine(PauseProwl(pauseDuration));
    }
    
    IEnumerator PauseProwl(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        doWalk = true;
        SwitchDirection();
    }

    private void ScanForPlayer()
    {
        Vector2 origin = guardObj.transform.position + new Vector3(1.6f * walkDirection, GuardController.eyeLevel);
        RaycastHit2D raycastHit = Physics2D.Raycast(origin, Vector2.right * walkDirection, guardController.sightDistance, guardController.rayCastLayerMask);
        Debug.DrawLine(origin, origin + Vector2.right * walkDirection * guardController.sightDistance, Color.green, Time.deltaTime);

        if (!raycastHit)
            return;
        
        if (raycastHit.collider.gameObject.CompareTag("Player"))
            _animator.SetTrigger("Spotted");
    }
}

