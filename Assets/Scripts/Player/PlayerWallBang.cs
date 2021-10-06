using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallBang : StateMachineBehaviour
{
    private GameObject playerObj;
    private PlayerController playerController;
    private AudioSource playerAudioSource;

    private bool continueBanging = false;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerObj = animator.gameObject;
        playerController = playerObj.GetComponent<PlayerController>();
        playerAudioSource = playerObj.GetComponent<AudioSource>();
        continueBanging = true;

        playerController.StartCoroutine(HalfPeriodOffset());
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        continueBanging = false;
    }

    IEnumerator HalfPeriodOffset()
    {
        yield return new WaitForSeconds(0.9f  * playerController.headBangPeriod);
        playerController.StartCoroutine(HeadBangCycle());
    }
    
    IEnumerator HeadBangCycle()
    {
        yield return new WaitForSeconds(playerController.headBangPeriod);

        if (playerObj.GetComponent<HeadButt>().isHeadButting)
            yield break;
        
        playerAudioSource.clip = playerController.headBangAudioClip;
        playerAudioSource.Play();

        if (continueBanging)
            playerController.StartCoroutine(HeadBangCycle());
    }
}
