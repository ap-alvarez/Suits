using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBoxBehaviour : EnemyBehaviour
{
    private Navigator nav;
    private FollowCommand follow;
    private EnemyWeapon explosion;
    private bool makeExplode = false;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        nav = GetComponent<Navigator>();
        follow = new FollowCommand(nav.GetController(), 5f, player, 5f);
        explosion = GetComponent<EnemyExplosion>();
        damagableBehaviour.damagable = new SuicideBoxDamagable(this, 10, 0, 0);
        nav.SetMoveCommand(follow);
    }

    // Update is called once per frame
    void Update()
    {
        if (follow.ReachedTarget() || makeExplode)
            Explode();
    }

    public void MakeExplode()
    {
        makeExplode = true;
    }

    private void Explode()
    {
        explosion.Attack();
    }
}
