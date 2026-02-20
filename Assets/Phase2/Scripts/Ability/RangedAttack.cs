using UnityEngine;

public class RangedAttack : MonoBehaviour, IAbility
{
    [SerializeField] private float ShootDamage = 20f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Projectile _projectilePrefab;

    private float _nextUseTime;
    public AbilityType AbilityType => AbilityType.Ranged;
    public float Cooldown => 10f;
    public bool IsReady => Time.time >= _nextUseTime;
    public void Active()
    {
        if (!IsReady)
        {
            return;
        }
        _nextUseTime = Time.time + Cooldown;
        var projectile = Instantiate(_projectilePrefab, transform.position + cameraTransform.forward * 2f, Quaternion.identity);
        projectile.Init(ShootDamage,  cameraTransform.forward.normalized);
    }
}
