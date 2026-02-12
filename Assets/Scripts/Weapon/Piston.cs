using UnityEngine;

public class Piston : WeaponBase
{
    public override void Fire()
    {
        Bullet bullet = PoolManager.Spawn<Bullet>(PoolType.bullet, transform.position, Quaternion.identity);
        if (bullet != null)
        {
            bullet.Init(3f, damage);
        }
        else
        {
            //Debug.Log("Piston.Fire()");
        }
    }
    
}
