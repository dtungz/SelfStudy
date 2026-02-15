using System;
using Interfaces;
using UnityEngine;

public class WP_Laser : MonoBehaviour, IWeapon
{
    [SerializeField] private float damage;
    [SerializeField] private float laserRange;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask enemyLayer;
    
    
    public float Damage => damage;
    public float Cooldown => 0f;
    public bool CanAttack => true;
    public WeaponType Type => WeaponType.Laser;
    private Ray ray;
    private RaycastHit[] hits = new RaycastHit[300];
    
    

    private void Start()
    {
        lineRenderer.enabled = false;
    }

    public void Attack()
    {
        DrawBeam();
        DamageAllInPath();
    }

    private void DrawBeam()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + transform.forward * laserRange);
    }
    
    public void Equip()
    {
        lineRenderer.enabled = true;
    }

    public void Unequip()
    {
        lineRenderer.enabled = false;
        
    }

    private void DamageAllInPath()
    {
        ray = new Ray(transform.position, transform.forward);
        int size = Physics.RaycastNonAlloc(ray, hits, laserRange,enemyLayer);
        for (int i = 0; i < size; i++ )
        {
            if (hits[i].collider.TryGetComponent<IDamageable>(out var enemy))
            {
                enemy.TakeDamage(damage * Time.deltaTime);
            }
        }
    }
    
}
