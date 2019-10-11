using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCommand : MoveCommand
{
    private GameObject target;
    private float distance;

    private bool reachedTarget;

    public FollowCommand(CharacterController controller, float moveSpeed, GameObject target, float distance) : base(controller, moveSpeed)
    {
        this.target = target;
        this.distance = distance;
    }
    // Start is called before the first frame update
    public override void Move()
    {
        Vector3 targetVect = VectorToTarget();
        reachedTarget = new Vector2(targetVect.x, targetVect.z).magnitude <= distance;
        if (!reachedTarget)
        {
            Vector3 moveVect = Vector3.ClampMagnitude(targetVect, moveSpeed * Time.deltaTime);
            moveVect += Physics.gravity * Time.deltaTime;
            controller.Move(moveVect);
            controller.transform.LookAt(target.transform, Vector3.up);
        }
    }

    public bool ReachedTarget()
    {
        return reachedTarget;
    }

    private Vector3 VectorToTarget()
    {
        Vector3 targetVect = target.transform.position - controller.transform.position;
        targetVect.y = 0;

        return targetVect;
    }
}
