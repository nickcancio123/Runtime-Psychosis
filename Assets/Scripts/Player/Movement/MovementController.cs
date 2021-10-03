using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GroundContact groundContact;

    private bool canMove = true;
    public bool CanMove() => canMove;

    private List<MovementModifier> modifiers = new List<MovementModifier>();
    public void AddModifier(MovementModifier modifier) => modifiers.Add(modifier);

    private void Update()
    {
        if (canMove)
        {
            foreach(MovementModifier mod in modifiers)
                mod.Move();
        }
    }
}
