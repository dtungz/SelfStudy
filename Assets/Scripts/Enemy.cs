using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("FIND SURVIVOR")]
    [SerializeField] private LayerMask survivorLayer;
    [SerializeField] private float findRadius;
    [SerializeField] private HealthComponent healthComponent;
    
    [Header("CONFIG")]
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float damage;
    private Transform _target = null;
    
    private void Update()
    {
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
        //Debug.Log("FindNearestTarget");
        Collider[] survivors = Physics.OverlapSphere(transform.position, findRadius, survivorLayer);
        Transform targetTf = null;
        for (int i = 0; i < survivors.Length; i++)
        {
            if (targetTf == null || Vector3.Distance(targetTf.position, transform.position) >
                Vector3.Distance(survivors[i].transform.position, transform.position))
            {
                targetTf = survivors[i].transform;
            }
        }
        return targetTf;
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
    
    // ----- VISUAL DEBUG -----
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findRadius);
    }
}
