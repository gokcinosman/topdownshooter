using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IMeleeWeapon
{
    public int Damage { get; private set; }
    public float Speed { get; private set; }
    public bool IsThrowable { get; private set; }
    public float KnockBackForce { get; private set; }
    public float Cooldown { get; private set; }

    private float currentCooldownTime = 0f;
    private void Start()
    {
        Damage = 10; // Example value
        Speed = 1.0f; // Example value
        KnockBackForce = 10.0f; // Example value
        Cooldown = 0.3f; // Example value
        IsThrowable = false; // Example value
    }





    public void Attack()
    {


    }



}
