using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModifier : MonoBehaviour
{
    public bool active;
    protected PlayerInput playerInput;
    [SerializeField] protected MovementController moveController;

    protected virtual void Awake()
    {
        playerInput = new PlayerInput();
        ReadInput();
    }

    void OnEnable()
    {
        playerInput.Enable();
        moveController.AddModifier(this);
    }
    
    void OnDisable() => playerInput.Disable();
    
    protected virtual void ReadInput() {}
    
    public virtual void Move() {}
}
