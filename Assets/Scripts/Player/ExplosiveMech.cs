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
        controller = GetComponent<MechController>();
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
    private void AimTowards(Vector3 worldPosition)
    {

        float headRotateSpeed = 100;
        float legRotateSpeed = 50;

        RotateMechPart(legs.transform, worldPosition, legRotateSpeed, 30);
        RotateMechPart(head.transform, worldPosition, headRotateSpeed, 5);




    }

    private Vector3 AimWeapons(Vector3 worldPos)
    {
        /*
        Vector3 localPos = transform.InverseTransformPoint(worldPos);
        Vector3 coneVect = Vector3.forward * 5000f;
        float aimRadius = 45;


        Vector3 projection = coneVect * (Vector3.Dot(localPos, coneVect) / coneVect.sqrMagnitude);
        float orthoDist = Mathf.Tan(aimRadius * Mathf.Deg2Rad) * projection.magnitude;
        if (Vector3.Distance(projection, localPos) > orthoDist)
        {
            Vector3 changeVect = Vector3.ClampMagnitude(localPos - projection, orthoDist);
            localPos = projection + changeVect;
        }
        

        worldPos = transform.TransformPoint(localPos);
        */
        Vector3 armLook = arms.transform.InverseTransformPoint(worldPos);
        armLook.x = 0;
        armLook = arms.transform.TransformPoint(armLook);

        Vector3 oldLook = arms.transform.forward;
        arms.transform.LookAt(armLook);
        Vector3 newLook = arms.transform.forward;
        float angle = Vector3.Angle(oldLook, newLook);
        float lookSpeed = 20f;
        float lerp = Mathf.Clamp01((lookSpeed * Time.deltaTime) / angle);
        arms.transform.rotation = Quaternion.LookRotation(Vector3.Lerp(oldLook, newLook, lerp));


        Vector3 aimPos = new Vector3();
        float magnitude = -1;
        foreach (PrimaryWeapon weapon in primaryWeapons)
        {
            Vector3 weaponAim = weapon.WeaponAim(worldPos);
            float weaponMagnitude = weapon.transform.InverseTransformPoint(weaponAim).magnitude;
            if (magnitude < 0 || weaponMagnitude < magnitude)
            {
                aimPos = weaponAim;
                magnitude = weaponMagnitude;
            }
        }
        return aimPos;
    }


    public override Vector3 MechAim(Vector3 worldPos)
    {
        AimTowards(worldPos);


        return AimWeapons(worldPos);
        /*
        Vector3 aimDir = arms.transform.TransformDirection(Vector3.forward);
        Ray aimRay = new Ray(arms.transform.position, aimDir);

        float minDistance = 5f;
        float maxDistance = 1000f;

        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        Vector3 hitPos = arms.transform.position + (maxDistance * aimDir);

        RaycastHit[] aimHits = Physics.RaycastAll(aimRay, maxDistance, layerMask);

        if (aimHits.Length > 0)
        {
            RaycastHit hit = aimHits[0];
            float hitDist = -1;
            for (int i = 0; i < aimHits.Length; i++)
            {
                RaycastHit newHit = aimHits[i];
                float dist = Vector3.Distance(arms.transform.position, newHit.point);
                if (dist < minDistance)
                    continue;
                if (hitDist < 0 || dist < hitDist)
                {
                    hit = newHit;
                    hitDist = dist;
                }
            }

            if (hitDist > 0)
            {
                hitPos = hit.point;
            }
            else
            {
                hitPos = arms.transform.position + (minDistance * aimDir);
            }
        }

        Debug.DrawLine(arms.transform.position, hitPos);
        return hitPos;
    }*/


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
