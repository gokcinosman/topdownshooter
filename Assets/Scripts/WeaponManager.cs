using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{


    public IWeapon currentWeapon;  // The single equipped weapon
    public GameObject weaponHolder; // Optional: A GameObject where weapons are attached in the scene
    private bool isColliding = false;
    public GameObject collidedWeapon;

    // Method to equip a new weapon
    public void EquipWeapon(IWeapon newWeapon)
    {
        currentWeapon = newWeapon;
        PlaceWeaponInHolder();
        Debug.Log("Equipped new weapon: " + newWeapon.GetType().Name);
    }
    public void UnequipWeapon()
    {
        currentWeapon = null;
        RemoveWeaponFromHolder();


    }

    // Places the weapon in the weapon holder
    public void PlaceWeaponInHolder()
    {
        if (weaponHolder == null)
        {
            Debug.LogWarning("Weapon holder is not set!");
            return;
        }

        if (currentWeapon is MonoBehaviour weaponObj)
        {
            weaponObj.transform.SetParent(weaponHolder.transform);
            weaponObj.transform.localPosition = Vector3.zero;
            weaponObj.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }
    }
    public void RemoveWeaponFromHolder()
    {
        if (weaponHolder == null)
        {
            Debug.LogWarning("Weapon holder is not set!");
            return;
        }

        if (currentWeapon is MonoBehaviour weaponObj)
        {
            weaponObj.transform.SetParent(null);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");
            EquipWeapon(collidedWeapon.GetComponent<WeaponPickup>().weaponToEquip);
            Debug.Log("Equipped weapon: " + collidedWeapon.GetComponent<WeaponPickup>().weaponToEquip);

        }
        HandleInput();
    }

    private void HandleInput()
    {
        if (currentWeapon == null)
            return;

        // Attacking with the current weapon
        if (Input.GetMouseButtonDown(0) && !IsReloading())
        {
            currentWeapon.Attack();
        }

        // For ranged weapons: handle reloading
        if (currentWeapon is IRangedWeapon rangedWeapon && Input.GetKeyDown(KeyCode.R) && !IsReloading())
        {
            StartCoroutine(ReloadRoutine(rangedWeapon));
        }

        // Throwing the weapon if it's throwable
        if (currentWeapon.IsThrowable && Input.GetMouseButtonDown(1))
        {
            ThrowWeapon();
        }


    }

    private bool IsReloading() => currentWeapon is IRangedWeapon rangedWeapon && rangedWeapon.IsReloading;

    private void ThrowWeapon()
    {
        Debug.Log("Throwing the weapon!");
        // Add logic for throwing the weapon (optional: remove the weapon after throwing)
        if (currentWeapon is MonoBehaviour weaponObj)
        {
            Debug.Log("Throwing weapon: " + weaponObj.name);
            UnequipWeapon();
            var weaponRb = weaponObj.GetComponent<Rigidbody2D>();
            weaponRb.isKinematic = false;
            weaponObj.transform.localRotation = Quaternion.Euler(180, 90, 0);
            weaponRb.AddForce(transform.up * 20, ForceMode2D.Impulse);
            float spinForce = 10f; // Adjust the spin force as needed
            weaponRb.AddTorque(spinForce, ForceMode2D.Impulse);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WeaponPickup"))
        {
            isColliding = true;
            collidedWeapon = other.gameObject;
            Debug.Log("Colliding with weapon: " + collidedWeapon.name);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WeaponPickup"))
        {
            isColliding = false;
            collidedWeapon = null;
        }


    }


    private IEnumerator ReloadRoutine(IRangedWeapon rangedWeapon)
    {
        Debug.Log("Reloading...");
        rangedWeapon.IsReloading = true;
        yield return new WaitForSeconds(rangedWeapon.ReloadTime);
        rangedWeapon.Reload();
        rangedWeapon.IsReloading = false;
        Debug.Log("Reload complete.");
    }
}
