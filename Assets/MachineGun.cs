using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : PrimaryWeapon
{
    public GameObject bullet;
    public Transform fireFrom;

    private float RPM = 600;
    private float damage = 10;
    private float bulletVelocity = 200;
    private float cooldown = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
    }

    public override void Fire()
    {

        if (cooldown > 60f / RPM)
        {
            Debug.Log("Try Fire");
            cooldown = 0;
            GameObject newBullet = Instantiate(bullet, fireFrom.position, fireFrom.rotation) as GameObject;
            newBullet.GetComponent<Bullet>().Fire(bulletVelocity, 10);
        }
    }

    public override void Reload()
    {
        throw new System.NotImplementedException();
    }
}
