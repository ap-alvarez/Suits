using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBoxDamagable : Damagable
{
    private SuicideBoxBehaviour behaviour;

    public SuicideBoxDamagable(SuicideBoxBehaviour behaviour, float health, float shield, float armor) : base(health, shield, armor)
    {
        this.behaviour = behaviour;
    }

    protected override void HealthZero()
    {
        behaviour.MakeExplode();
    }
}

