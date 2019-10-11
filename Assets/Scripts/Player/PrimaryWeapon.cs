using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrimaryWeapon : MonoBehaviour
{
    int ammo;

    public abstract void Fire();
    public abstract void Reload();
    public abstract Vector3 WeaponAim(Vector3 worldPos);
}
