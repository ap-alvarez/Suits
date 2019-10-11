using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    protected CharacterController controller;

    private MoveCommand moveCommand;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        Navigate();
    }

    private void Navigate()
    {
        if (moveCommand != null)
            moveCommand.Move();
    }

    public void SetMoveCommand(MoveCommand moveCommand)
    {
        this.moveCommand = moveCommand;
    }

    public MoveCommand GetMoveCommand()
    {
        return moveCommand;
    }

    public CharacterController GetController()
    {
        return controller;
    }
}
