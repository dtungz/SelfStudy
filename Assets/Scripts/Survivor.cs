using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;

public class Survivor : MonoBehaviour, IDamageable
{
    [Header("CONFIG")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private float findCoolDown;
    
    [Header("REFERENCES")]
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Weapon weapon;
    
    public FactionType Faction => FactionType.Survivor;
    public event Action FindEnemy;

    private Transform _targetTf;
    private float _scanTimmer;

    
    // ----- CORE LOOP -----
    private void Update()
    {
        LookToTarget();
        if(_scanTimmer >= Time.time)
            return;
        _scanTimmer = Time.time + findCoolDown;
        FindEnemy?.Invoke();
    }
    // ----- HEALPER -----
    
    private void LookToTarget()
    {
        if (_targetTf == null)
        {
            LookAhead(Vector3.forward);
        }
        else
        {
            Vector3 dir = _targetTf.position - transform.position;
            dir.y = 0; // nếu game top-down hoặc không muốn ngửa đầu lên
            LookAhead(dir);
        }
    }
    
    private void LookAhead(Vector3 direction)
    {
        direction = new Vector3(direction.x, 0, direction.z);
        Quaternion targetRot = Quaternion.LookRotation(direction.normalized);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            RotateSpeed * Time.deltaTime
        );
    }

    
    // ----- PUBLIC METHODS -----
    public void TakeDamage(float amount)
    {
        healthComponent.TakeDamage(amount);
    }

    public void Heal(float amount)
    {
        healthComponent.Heal(amount);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void MoveHorizontal(float direction)
    {
        controller.Move(Vector3.right * (direction * moveSpeed * Time.deltaTime));
    }

    public void SetTarget(Transform target)
    {
        _targetTf = target;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void AddFindEnemyEvent(Action action)
    {
        if (action == null)
            return;
        FindEnemy += action;
    }

    public void RemoveFindEnemyEvent(Action action)
    {
        if(action == null)
            return;
        FindEnemy -=  action;
    }
}
