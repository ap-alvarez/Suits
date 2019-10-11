using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { primary, secondary, tertiary }

public abstract class MechActor : MonoBehaviour
{

    protected float shield;
    protected float health;
    protected Transform[] mounts;
    protected MechController controller;

    public abstract void Attack(AttackType attackType);
    public abstract void Move(Vector3 direction);

    /// <summary>
    /// Aims the mech 
    /// </summary>
    /// <returns>The world position the mech is currently aiming at</returns>
    public abstract Vector3 MechAim(Vector3 worldPos);

}
