using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : EnemyWeapon
{
    public float explosionRadius;
    public GameObject explosion;

    public override bool Attack()
    {
        bool willAttack = base.Attack();
        if (willAttack)
            Explode();
        return willAttack;
    }

    public void Explode()
    {
        Collider[] others = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider other in others)
        {
            if (other.gameObject.Equals(gameObject))
                continue;
            DamagableBehaviour damagableBehaviour = other.GetComponent<DamagableBehaviour>();
            if (damagableBehaviour != null)
                damagableBehaviour.damagable.TakeDamage(damage, damageType);
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
