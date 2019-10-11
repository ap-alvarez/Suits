using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : PrimaryWeapon
{
    public GameObject bullet;
    public GameObject barrelEnd;
    public GameObject barrel;

    private float RPM = 600;
    private float damage = 10;
    private float bulletVelocity = 400f;

    private float warmUpTime = .5f;
    private float coolDownWait = .1f;
    private bool coolingDown = false;
    private float barrelRotSpeed = 600;

    private AudioSource audioSource;
    private Light lightSource;

    private float lightIntensity = 9f;
    private float lightLife = .1f;

    private float fireWaitTotal;
    private float fireWait = 0;
    private float warmUp = 0;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lightSource = barrelEnd.GetComponent<Light>();
        fireWaitTotal = (60f / RPM);
    }

    // Update is called once per frame
    void Update()
    {
        fireWait += Time.deltaTime;
        lightSource.intensity = lightIntensity * (1 - Mathf.Clamp(fireWait / fireWaitTotal, 0, 1));

        if (fireWait - coolDownWait > fireWaitTotal * (warmUp / warmUpTime))
            coolingDown = true;

        if (coolingDown)
        {
            if (warmUp > 0)
                warmUp -= Time.deltaTime;
            else if (warmUp < 0)
                warmUp = 0;

        }

        barrel.transform.Rotate(Vector3.forward, (1 - WarmUpModifier()) * barrelRotSpeed, Space.Self);
    }

    public override void Fire()
    {
        coolingDown = false;

        if (warmUp < warmUpTime)
            warmUp = Mathf.Clamp(warmUp + Time.deltaTime, 0, warmUpTime);



        if (fireWait - (.1f * WarmUpModifier()) > 60f / RPM)
        {
            fireWait = 0;
            FireBullet();
        }
    }

    public override void Reload()
    {
        throw new System.NotImplementedException();
    }

    private void AimAt(Vector3 worldPosition)
    {
        Vector3 oldLook = transform.forward;
        transform.LookAt(worldPosition);
        Vector3 newLook = transform.forward;
        float angle = Vector3.Angle(oldLook, newLook);
        float lookSpeed = 10f;
        float lerp = Mathf.Clamp01((lookSpeed * Time.deltaTime) / angle);
        transform.rotation = Quaternion.LookRotation(Vector3.Lerp(oldLook, newLook, lerp));
    }

    public override Vector3 WeaponAim(Vector3 worldPos)
    {
        float maxDist = 5000;

        Vector3 localAim = transform.InverseTransformPoint(worldPos);

        if (localAim.magnitude >= barrelEnd.transform.localPosition.magnitude)
        {
            AimAt(worldPos);
        }

        Vector3 currentAim = transform.position + (transform.forward * maxDist);
        Ray aimRay = new Ray(barrelEnd.transform.position, transform.forward);
        RaycastHit hit;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        if (Physics.Raycast(aimRay, out hit, maxDist, layerMask))
        {
            currentAim = hit.point;
        }

        return currentAim;
    }

    private void FireBullet()
    {
        GameObject newBullet = Instantiate(bullet, barrelEnd.transform.position, barrelEnd.transform.rotation) as GameObject;
        newBullet.GetComponent<Bullet>().Fire(bulletVelocity, 10);

        audioSource.Play();
    }

    private float WarmUpModifier()
    {
        float warmUpVal = warmUp / warmUpTime;
        warmUpVal *= warmUpVal;
        warmUpVal = 1 - warmUpVal;

        return warmUpVal;
    }
}
