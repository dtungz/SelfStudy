using UnityEngine;

public interface IWeapon
{
    float Damage { get; }
    float Cooldown { get; }
    bool CanAttack { get; }
    
    WeaponType Type { get; }
    
    void Attack();
    void Equip();
    void Unequip();
}
