
public interface IWeapon
{
    int Damage { get; }
    float Speed { get; }
    bool IsThrowable { get; }
    void Attack();

}

public interface IMeleeWeapon : IWeapon
{
    float KnockBackForce { get; }
}
public interface IRangedWeapon : IWeapon
{
    int AmmoCount { get; }
    int MaxAmmoCount { get; }
    int MultiShotCount { get; }
    float ReloadTime { get; }
    void Reload();
}
