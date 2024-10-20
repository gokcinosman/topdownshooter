using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    public IWeapon weaponToEquip;
    private void Start()
    {
        weaponToEquip = GetComponentInParent<IWeapon>();
    }
}
