using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrimaryWeapon : MonoBehaviour
{
    int ammo;

    public abstract void Fire();
    public abstract void Reload();
}
