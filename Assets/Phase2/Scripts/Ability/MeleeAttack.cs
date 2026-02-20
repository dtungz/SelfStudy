using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack : MonoBehaviour, IAbility
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float hitForce;
    [SerializeField] private float attackRange;
    public float Cooldown => 1f;
    public bool IsReady => Time.time >= _nextUseTime;
    public AbilityType AbilityType => AbilityType.Melee;
    
    private float _nextUseTime;
    public void Active()
    {
        if (!IsReady)
            return;
        _nextUseTime = Time.time + Cooldown;
        Attack();
    }

    private void Attack()
    {
        Ray ray =  _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, attackRange))
        {
            IHitable hitable = hit.collider.gameObject.GetComponent<IHitable>();
            if (hitable == null)
                return;
            HitPayload hitPayload = new HitPayload
            {
                HitPoint = hit.point,
                Direction = ray.direction.normalized,
                HitForce = hitForce,
                ForceMode = ForceMode.Impulse,
            };
            
            hitable.Hit(hitPayload);
        }
    }
}
