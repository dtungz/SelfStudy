using Interfaces;
using Phase1;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
    protected override void OnAttack()
    {
        Collider[] survivors = Physics.OverlapSphere(attackPoint.position, AttackRange/2, survivorLayer);
        foreach (var survivor in survivors)
        {
            if (survivor.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(Damage);
            }
        }
    }
}
