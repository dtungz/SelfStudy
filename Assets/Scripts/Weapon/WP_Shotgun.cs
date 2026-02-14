using System;
using UnityEngine;

public class WP_Shotgun : MonoBehaviour, IWeapon
{
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    [SerializeField] private int bullets;
    [SerializeField] private float angle;
    
    private float nextAttackTime;
    
    public float Damage => damage;
    public float Cooldown => cooldown;
    public bool CanAttack => Time.time >= nextAttackTime;
    public WeaponType Type => WeaponType.Shotgun;


    public void Attack()
    {
        if (!CanAttack) return;
        nextAttackTime = Time.time + cooldown;
        Fire();
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
        float startAngle = -angle/2;
        float stepAngle = angle/(bullets - 1);
        for (int i = 0; i < bullets; i++)
        {
            float angle =  startAngle + stepAngle * i;
            Quaternion rot = Quaternion.Euler(0, angle, 0) * transform.rotation;
            Bullet bullet = PoolManager.Spawn<Bullet>(PoolType.bullet,  transform.position, rot);
            bullet.Init(3f, damage);
        }
    }
}
