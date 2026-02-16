using UnityEngine;

public class WP_Piston: MonoBehaviour, IWeapon, IModifiable
{
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;

    private float nextAttackTime;
    
    public float Damage => damage;
    public float Cooldown => cooldown;
    public WeaponType Type => WeaponType.Piston;
    public bool CanAttack => Time.time >= nextAttackTime;
    
    public void Attack()
    {
        if (!CanAttack)
            return;
        nextAttackTime =  Time.time + cooldown;
        Fire();
    }

    public void ApplyModifier(float modifier)
    {
        damage = Mathf.RoundToInt(modifier * damage);
    }

    public void Equip()
    {
        nextAttackTime = Time.time;
    }

    public void Unequip()
    {
        nextAttackTime = Time.time + cooldown;
    }

    private void Fire()
    {
        Bullet bullet = PoolManager.Spawn<Bullet>(PoolType.bullet, transform.position, transform.rotation);
        bullet.Init(3f, damage, FactionType.Survivor);
    }
    
}