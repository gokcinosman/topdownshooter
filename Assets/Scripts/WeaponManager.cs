using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponManager : MonoBehaviour
{


    public IWeapon currentWeapon;  // The single equipped weapon
    public GameObject weaponHolder; // Optional: A GameObject where weapons are attached in the scene
    private bool isColliding = false;
    public GameObject collidedWeapon;
    private float attackTimer;

    // Method to equip a new weapon
    public void EquipWeapon(IWeapon newWeapon)
    {
        currentWeapon = newWeapon;
        if (currentWeapon is MonoBehaviour weaponObj)
        {
            weaponObj.GetComponent<Collider2D>().enabled = false;
            EquipSprite(weaponObj.GetComponent<IWeapon>().equippedSprite);
        }
        attackTimer = currentWeapon.Cooldown;

        PlaceWeaponInHolder();
        Debug.Log("Equipped new weapon: " + newWeapon.GetType().Name);
    }
    public void EquipSprite(Sprite newSprite)
    {
        if (currentWeapon is MonoBehaviour weaponObj)
        {
            weaponObj.GetComponent<SpriteRenderer>().sprite = weaponObj.GetComponent<IWeapon>().equippedSprite;
        }
    }
    public void UnequipSprite(Sprite newSprite)
    {
        if (currentWeapon is MonoBehaviour weaponObj)
        {
            weaponObj.GetComponent<SpriteRenderer>().sprite = weaponObj.GetComponent<IWeapon>().unequippedSprite;
        }
    }
    public void UnequipWeapon()
    {
        if (currentWeapon is MonoBehaviour weaponObj)
        {
            weaponObj.GetComponent<Collider2D>().enabled = true;
            UnequipSprite(weaponObj.GetComponent<IWeapon>().unequippedSprite);
        }

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
            weaponObj.transform.localRotation = Quaternion.Euler(Vector3.zero);

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
        if (Input.GetKeyDown(KeyCode.E) && isColliding)
        {
            EquipWeapon(collidedWeapon.GetComponent<WeaponPickup>().weaponToEquip);
        }

        HandleInput();
    }

    private void HandleInput()
    {
        if (currentWeapon == null)
            return;

        // Saldırı zamanlayıcısını güncelle

        attackTimer -= Time.deltaTime;
        // Eğer sol fare tuşuna basılı tutuluyorsa ve cooldown süresi dolmuşsa
        if (Input.GetMouseButton(0) && attackTimer <= 0)
        {
            currentWeapon.Attack();
            attackTimer = currentWeapon.Cooldown;
        }

        // Silahı fırlatma kontrolü
        if (currentWeapon.IsThrowable && Input.GetMouseButtonDown(1))
        {
            ThrowWeapon();
        }
    }



    private void ThrowWeapon()
    {
        Debug.Log("Throwing the weapon!");
        RemoveWeaponFromHolder();

        if (currentWeapon is MonoBehaviour weaponObj)
        {
            Debug.Log("Throwing weapon: " + weaponObj.name);
            UnequipWeapon();

            var weaponRb = weaponObj.GetComponent<Rigidbody2D>();
            if (weaponRb != null)
            {
                weaponRb.isKinematic = false;
                weaponRb.gravityScale = 1f; // Yerçekimi etkisini aktif et
                weaponRb.drag = 2f; // Yavaşlama etkisini artır
                weaponRb.AddForce(transform.up * 20, ForceMode2D.Impulse);
                float spinForce = 10f;
                weaponRb.AddTorque(spinForce, ForceMode2D.Impulse);
                StartCoroutine(MakeWeaponKinematic(weaponRb));


            }
        }
    }
    private IEnumerator MakeWeaponKinematic(Rigidbody2D weaponRb)
    {
        yield return new WaitForSeconds(1f); // Bekleme süresi

        weaponRb.velocity = Vector2.zero; // Hızı sıfırla
        weaponRb.angularVelocity = 0f; // Dönme hızını sıfırla
        weaponRb.isKinematic = true; // Kinematik hale getir
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



}
