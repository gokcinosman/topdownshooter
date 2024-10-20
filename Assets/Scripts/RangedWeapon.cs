using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : IRangedWeapon
{
    public int Damage { get; private set; }
    public float Speed { get; private set; }
    public bool IsThrowable { get; private set; }
    public int AmmoCount { get; private set; }
    public float ReloadTime { get; private set; }
    public int MaxAmmoCount { get; private set; }

    public int MultiShotCount { get; private set; }

    public RangedWeapon(int damage, float speed, int ammoCount, float reloadTime, bool isThrowable, int maxAmmoCount, int multiShotCount)
    {
        Damage = damage;
        Speed = speed;
        AmmoCount = ammoCount;
        ReloadTime = reloadTime;
        IsThrowable = isThrowable;
        MaxAmmoCount = maxAmmoCount;
        MultiShotCount = multiShotCount;
    }


    public void Attack()
    {
        // Ranged attack logic
        if (AmmoCount > 0)
        {
            AmmoCount--;
        }

    }

    public void Reload()
    {
        // Reload logic
    }
}