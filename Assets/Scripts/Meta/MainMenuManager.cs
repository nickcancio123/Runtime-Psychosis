using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Level1CameraScript level1CamScript;
    [SerializeField] private GameObject mainMenuUI;
    
    private PlayerInput playerInput;
    
    private void OnEnable() => playerInput.Enable();
    private void OnDisable() => playerInput.Disable();

    private void Awake()
    {
        playerInput = new PlayerInput();
        ReadInput();
    }

    private void ReadInput()
    {
        playerInput.Interact.HeadButt.performed += ctx => OnHeadButt();
    }

    private void OnHeadButt()
    {
        player.GetComponent<Animator>().SetTrigger("HeadButt");
        player.GetComponent<MovementController>().enabled = true;
        
        level1CamScript.inMainMenu = false;
        mainMenuUI.SetActive(false);
        
        //DESTROY WALL
    }
}
