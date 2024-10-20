public interface IWeapon
{
    int Damage { get; }
    float Speed { get; }
    bool IsThrowable { get; }
    float Cooldown { get; }
    void Attack();
}

public interface IMeleeWeapon : IWeapon
{
    float KnockBackForce { get; }
}

public interface IRangedWeapon : IWeapon
{
    int AmmoCount { get; }
    int MultiShotCount { get; }



}
