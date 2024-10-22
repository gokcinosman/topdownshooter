using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour, IRangedWeapon
{
    public int Damage { get; private set; }
    public float Speed { get; private set; }
    public bool IsThrowable { get; private set; }
    public int AmmoCount { get; private set; }
    public int MultiShotCount { get; private set; }
    public float Cooldown { get; private set; }
    [SerializeField]
    private Sprite _equippedSprite;
    [SerializeField]
    private Sprite _unequippedSprite;

    public Sprite equippedSprite
    {
        get => _equippedSprite;
        set => _equippedSprite = value;
    }

    public Sprite unequippedSprite
    {
        get => _unequippedSprite;
        set => _unequippedSprite = value;
    }
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint; // Where projectiles are spawned
    public float projectileForce = 10f;
    void Awake()
    {
        // Initialize weapon properties
        Damage = 10; // Example value
        Speed = 1.0f; // Example value
        AmmoCount = 10; // Example value
        Cooldown = 0.2f; // Example value
        MultiShotCount = 1; // Example value
        IsThrowable = true; // Example value
    }

    public void Attack()
    {
        if (AmmoCount <= 0)
        {
            return;
        }

        // Ranged attack logic
        for (int i = 0; i < MultiShotCount; i++)
        {
            SpawnProjectile();
        }
        AmmoCount--;
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


    }


}
