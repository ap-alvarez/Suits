using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { physical, energy, trueDamage }

public abstract class Damagable
{
    private float shield;
    private float health;
    private float armor;

    private float currentHealth;
    private float currentShield;

    // Start is called before the first frame update
    public Damagable(float health, float shield, float armor)
    {
        this.health = health;
        this.shield = shield;
        this.armor = armor;

        currentHealth = health;
        currentShield = shield;
    }

    public virtual void TakeDamage(float damage, DamageType type)
    {
        switch (type)
        {
            case (DamageType.physical):
                damage = DamageShield(damage, 1f);
                DamageHealth(damage, (1 - Mathf.Clamp01(armor / damage)));
                break;
            case (DamageType.energy):
                DamageShield(damage, 3f);
                break;
            case (DamageType.trueDamage):
                DamageHealth(damage, 1f);
                break;

        }
    }

    protected virtual void ShieldChanged(float difference) { }
    protected virtual void ShieldZero() { }

    protected virtual void HealthChanged(float difference) { }
    protected virtual void HealthZero() { }

    private float DamageShield(float damage, float mod)
    {
        damage *= mod;
        if (damage >= currentShield)
        {
            currentShield = 0;
            ShieldZero();
            return damage - currentShield;
        }
        else
        {
            currentShield -= damage;
            ShieldChanged(damage);
            return 0;
        }
    }

    private void DamageHealth(float damage, float mod)
    {
        damage *= mod;
        if (damage >= currentHealth)
        {
            currentHealth = 0;
            HealthZero();
        }
        else
        {
            currentHealth -= damage;
            HealthChanged(damage);
        }
    }


}
