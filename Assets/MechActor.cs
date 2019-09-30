using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { primary, secondary, tertiary }

public abstract class MechActor : MonoBehaviour
{

    protected float shield;
    protected float health;
    protected MountableObject[] mountedObjects;
    protected Transform[] mounts;

    public abstract void Attack(AttackType attackType);
    public abstract void Move(Vector3 direction);
    public abstract void AimTowards(Vector3 position);

}
