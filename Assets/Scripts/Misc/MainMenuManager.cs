using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private CinemachineVirtualCamera mainMenuVCAM;
    [SerializeField] private TextMeshProUGUI gameTitleUI;
    [SerializeField] private GameObject instructionsUI;
    [SerializeField] private CinemachineVirtualCamera level1VCAM;
    [SerializeField] private MainMenuCameraScript mainMenuCameraScript;
    [SerializeField] private AudioClip headButtImpact;
    [SerializeField] private float mainTextFadeStartDelay = 5;
    [SerializeField] private float mainTextFadeDuration = 2;
    
    private PlayerInput playerInput;
    private bool doFadeMainText = false;
    private float mainTextStartTime = 0;

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
        player.GetComponent<Animator>().SetBool("inMainMenu", true);
        gameTitleUI.color = new Color(255, 255, 255, 0);

        instructionsUI.SetActive(false);
        StartCoroutine(StartMainTextFade());
    }

    private void Update()
    {
        if (doFadeMainText)
        {
            if (gameTitleUI.color.a < 1)
            {
                gameTitleUI.color = 
                    new Color(255, 255, 255, gameTitleUI.color.a + Time.deltaTime / mainTextFadeDuration);
            }
            else
            {
                doFadeMainText = false;
                if (!instructionsUI.activeInHierarchy)
                    instructionsUI.SetActive(true);
            }
        }
    }

    private void ReadInput()
    {
        playerInput.Interact.HeadButt.performed += ctx => OnHeadButt();
    }

    private void OnHeadButt()
    {
        if (!mainMenuCameraScript.DonePanning())
            return;
        
        player.GetComponent<Animator>().SetBool("inMainMenu", false);
        player.GetComponent<Animator>().SetTrigger("HeadButt");
        StartCoroutine(player.GetComponent<HeadButt>().LungeDelay());
        StartCoroutine(HeadButtDelay());
        
        //DESTROY WALL
    }

    IEnumerator HeadButtDelay()
    {
        yield return new WaitForSeconds(0.75f);
        
        mainMenuUI.SetActive(false);

        player.GetComponent<MovementController>().enabled = true;
        
        level1VCAM.Priority = 2;
        mainMenuVCAM.Priority = 1;

        this.enabled = false;
    }

    IEnumerator StartMainTextFade()
    {
        yield return new WaitForSeconds(mainTextFadeStartDelay);
        doFadeMainText = true;
        mainTextStartTime = Time.time;
    }
}
