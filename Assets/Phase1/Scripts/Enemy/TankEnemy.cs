using Interfaces;
using Phase1;
using UnityEngine;

public class TankEnemy : EnemyBase
{
    protected override void OnAttack()
    {
        //Debug.Log("OnAttack called");
        bool checkHit = false;
        Collider[] survivors = Physics.OverlapSphere(attackPoint.position, AttackRange/2, survivorLayer);
        foreach (var survivor in survivors)
        {
            if (survivor.TryGetComponent(out IDamageable damageable))
            {
                //Debug.Log("Attacking " + damageable);
                damageable.TakeDamage(Damage);
                checkHit = true;
            }
        }

        if (checkHit)
        {
            //Debug.Log("Heal");
            healthComponent.Heal(Damage);
        }
    }
}
