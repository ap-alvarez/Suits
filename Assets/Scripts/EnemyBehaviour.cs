using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected GameObject player;
    protected DamagableBehaviour damagableBehaviour;

    public virtual void Start()
    {
        damagableBehaviour = GetComponent<DamagableBehaviour>();
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
