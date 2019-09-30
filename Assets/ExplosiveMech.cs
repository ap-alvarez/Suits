using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMech : MechActor
{
    public GameObject legs;
    public GameObject head;
    public GameObject arms;

    public PrimaryWeapon[] primaryWeapons;

    private CharacterController moveHandler;
    // Use this for initialization
    void Start()
    {
        moveHandler = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Attack(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.primary:
                foreach (PrimaryWeapon weapon in primaryWeapons)
                {
                    weapon.Fire();
                }
                break;
        }
    }
    public override void Move(Vector3 direction)
    {
        float moveSpeed = 3.5f;

        Vector3 moveDir = legs.transform.TransformDirection(direction);

        moveSpeed *= Mathf.Clamp(direction.magnitude, 0, 1) * Time.deltaTime;
        moveHandler.Move(moveDir.normalized * moveSpeed);
    }
    public override void AimTowards(Vector3 worldPosition)
    {

        float headRotateSpeed = 100;
        float legRotateSpeed = 50;

        RotateMechPart(legs.transform, worldPosition, legRotateSpeed, 30);
        RotateMechPart(head.transform, worldPosition, headRotateSpeed, 5);

        Vector3 armLook = arms.transform.InverseTransformPoint(worldPosition);
        armLook.x = 0;
        arms.transform.LookAt(arms.transform.TransformPoint(armLook));


    }

    private void RotateMechPart(Transform part, Vector3 worldPosition, float rotationSpeed, float buffer)
    {
        Vector3 look = part.InverseTransformPoint(worldPosition);
        look.y = 0;

        float maxRot = rotationSpeed * Time.deltaTime;
        float angleDif = Mathf.Clamp(Vector3.SignedAngle(Vector3.forward, look, Vector3.up), -maxRot, maxRot);

        //float newRotation = Mathf.MoveTowardsAngle(part.eulerAngles.y, part.eulerAngles.y + angleDif, rotationSpeed * Time.deltaTime);
        part.Rotate(Vector3.up, angleDif);
    }
}
