using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private CinemachineVirtualCamera mainMenuVCAM;
    [SerializeField] private CinemachineVirtualCamera level1VCAM;
    [SerializeField] private MainMenuCameraScript mainMenuCameraScript;
    [SerializeField] private AudioClip headButtImpact;
    
    private PlayerInput playerInput;
    
    private void OnEnable() => playerInput.Enable();
    private void OnDisable() => playerInput.Disable();

    private void Awake()
    {
        playerInput = new PlayerInput();
        ReadInput();
    }

    private void Start()
    {
        player.GetComponent<MovementController>().enabled = false;
    }

    private void ReadInput()
    {
        playerInput.Interact.HeadButt.performed += ctx => OnHeadButt();
    }

    private void OnHeadButt()
    {
        if (!mainMenuCameraScript.DonePanning())
            return;
        
        player.GetComponent<Animator>().SetTrigger("HeadButt");
        player.GetComponent<MovementController>().enabled = true;
        StartCoroutine(HeadButtDelay());
        player.GetComponent<CameraShake>().Shake(mainMenuVCAM);
        mainMenuUI.SetActive(false);
        
        //DESTROY WALL
    }

    IEnumerator HeadButtDelay()
    {
        yield return new WaitForSeconds(0.75f);
        
        player.GetComponent<AudioSource>().clip = headButtImpact;
        player.GetComponent<AudioSource>().Play();
        
        level1VCAM.Priority = 2;
        mainMenuVCAM.Priority = 1;
    }
}
