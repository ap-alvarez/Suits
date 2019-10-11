using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float cooldown;
    public float damage;
    public DamageType damageType;

    protected float cooldownCurrent;

    public virtual void Update()
    {
        cooldownCurrent += Time.deltaTime;
    }

    /// <summary>
    /// asks the weapon to fire this frame
    /// </summary>
    public virtual bool Attack()
    {
        bool willAttack = (cooldownCurrent >= cooldown);
        if (willAttack)
            cooldownCurrent = 0;
        return willAttack;
    }

    /// <summary>
    /// gets the current cooldown of the weapon. Clamped [0,1]
    /// </summary>
    /// <returns></returns>
    public virtual float GetCooldown()
    {
        return Mathf.Clamp01(cooldownCurrent / cooldown);
    }
}
