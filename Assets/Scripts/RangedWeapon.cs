using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour, IRangedWeapon
{
    public int Damage { get; private set; }
    public float Speed { get; private set; }
    public bool IsThrowable { get; private set; }
    public int AmmoCount { get; private set; }
    public float ReloadTime { get; private set; }
    public int MaxAmmoCount { get; private set; }
    public int MultiShotCount { get; private set; }
    public bool IsReloading { get; set; }  // New property

    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint; // Where projectiles are spawned
    public float projectileForce = 10f;
    void Awake()
    {
        // Initialize weapon properties
        Damage = 10; // Example value
        Speed = 1.0f; // Example value
        AmmoCount = 5; // Example value
        ReloadTime = 2.0f; // Example value
        MaxAmmoCount = 10; // Example value
        MultiShotCount = 1; // Example value
        IsThrowable = true; // Example value
    }

    public void Attack()
    {
        if (AmmoCount <= 0 || IsReloading)
        {
            Debug.Log("No ammo or reloading!");
            return;
        }

        // Ranged attack logic
        for (int i = 0; i < MultiShotCount; i++)
        {
            SpawnProjectile();
        }
        AmmoCount--;
        Debug.Log("Shot fired! Ammo left: " + AmmoCount);
    }

    public void Reload()
    {
        Debug.Log("Reloading weapon...");
        AmmoCount = MaxAmmoCount; // Refill ammo
    }

    private void SpawnProjectile()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("ProjectilePrefab or FirePoint not set!");
            return;
        }

        // Instantiate the projectile at the firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Apply force to the projectile's Rigidbody2D
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("Projectile does not have a Rigidbody2D component!");
        }
    }
}
