using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IMeleeWeapon
{
    public int Damage { get; private set; }
    public float Speed { get; private set; }
    public bool IsThrowable { get; private set; }
    public float KnockBackForce { get; private set; }



    public MeleeWeapon(int damage, float speed, float knockbackForce, bool isThrowable)
    {
        Damage = damage;
        Speed = speed;
        KnockBackForce = knockbackForce;
        IsThrowable = isThrowable;
    }

    public void Attack()
    {
        // Melee attack logic
    }
}
