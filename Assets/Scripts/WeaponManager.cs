using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private IWeapon currentWeapon;  // The single equipped weapon
    public GameObject weaponHolder; // Optional: A GameObject where weapons are attached in the scene

    // Method to equip a new weapon
    public void EquipWeapon(IWeapon newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log("Equipped new weapon: " + newWeapon.GetType().Name);
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (currentWeapon == null)
            return;

        // Attacking with the current weapon
        if (Input.GetButtonDown("Fire1"))
        {
            currentWeapon.Attack();
        }

        // For ranged weapons: handle reloading
        if (currentWeapon is IRangedWeapon && Input.GetButtonDown("Reload"))
        {
            ((IRangedWeapon)currentWeapon).Reload();
        }

        // Throwing the weapon if it's throwable
        if (currentWeapon.IsThrowable && Input.GetButtonDown("Fire2"))
        {
            ThrowWeapon();
        }
    }

    private void ThrowWeapon()
    {
        Debug.Log("Throwing the weapon!");

        // Add logic for throwing the weapon (optional: remove the weapon after throwing)
        currentWeapon = null;  // Unequip the weapon after throwing if necessary
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WeaponPickup"))
        {
            // Assuming the weapon pickup object has a script that holds a reference to the weapon to equip
            IWeapon pickedWeapon = other.GetComponent<WeaponPickup>().weaponToEquip;

            EquipWeapon(pickedWeapon);

            // Destroy the weapon pickup object after equipping
            Destroy(other.gameObject);
        }
    }

}

