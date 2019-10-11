using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveCommand
{
    protected float moveSpeed;

    protected CharacterController controller;

    public MoveCommand(CharacterController controller, float moveSpeed)
    {
        this.controller = controller;
        this.moveSpeed = moveSpeed;
    }

    public abstract void Move();
}
