using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private EntityManager entityManager;
    
    
    [Header("CONFIG")]
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float findSurvivorCooldown;
    private Transform _target;
    private float _timeSinceLastFindSurvivor;

    private void Start()
    {
        _target = null;
        _timeSinceLastFindSurvivor = 0f;
    }

    private void Update()
    {
        FindNearestTarget();
        Move();
    }

    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
    }
    
    private void Move()
    {
        FindSurvivor();
        transform.position += transform.forward * (speed * Time.deltaTime);
    }
    
    // ----- PRIVATE HEALPER -----
    private void FindSurvivor()
    {
        // This method is make enemy rotate to survivor or look to -z
        Vector3 targetDirection;
        if (_target == null)
        {
            targetDirection = Vector3.back;
            _target = FindNearestTarget();
        }
        else
        {
            targetDirection = _target.position - transform.position;
        }

        targetDirection.y = 0f; 

        if (targetDirection != Vector3.zero) 
        {
            targetDirection.Normalize();
            LookAhead(targetDirection);
        }
    }
    private Transform FindNearestTarget()
    {
        while (_timeSinceLastFindSurvivor < findSurvivorCooldown)
        {
            _timeSinceLastFindSurvivor += Time.deltaTime;
        }
        return entityManager.GetSurvivorNearsest(transform.position);
    }

    private void LookAhead(Vector3 direction)
    {
        Quaternion targetRot = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotateSpeed * Time.deltaTime
        );
    }
}
