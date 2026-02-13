using System;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public enum ZombieType{Basic, Fast, Tank}
    
    [Header("TYPE")]
    [SerializeField] protected ZombieType zombieType;
    
    
    [Header("REFERENCES")]
    [SerializeField] protected HealthComponent healthComponent;
    [SerializeField] protected EntityManager entityManager;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask survivorLayer;
    [SerializeField] protected ChangeColorBaloWhenEnemyAttack whenEnemyAttack;
    
    
    [Header("CONFIG")]
    [SerializeField] private DataConfigSO config;
    
    protected float Speed;
    protected float RotateSpeed;
    protected float Damage;
    protected float AttackRange;
    protected float FindSurvivorCooldown;
    protected float AttackSurvivorCooldown;
    
    protected Transform _target;
    protected float _attackTimer;
    protected float _findTimer;

    private void Awake()
    {
        Initalize();
    }
    
    // ----- CORE LOOP -----
    private void Update()
    {
        Tick();
        if (_target == null || Vector3.Distance(transform.position, _target.position) > AttackRange)
        {
            MoveLogic();
        }
        else
        {
            AttackLogic();
        }
        
    }
    
    // ----- TEMPLETE METHODS ----- 

    protected virtual void Tick() { }
    
    private void AttackLogic()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer < AttackSurvivorCooldown)
            return;
        _attackTimer = 0;
        whenEnemyAttack.ChangeColor();
        OnAttack();
    }
    
    private void MoveLogic()
    {
        // Look to target if target != null and look to back if target == null
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
        
        // Move to forward
        transform.position += transform.forward * (Speed * Time.deltaTime);
        OnMove();
    }
    
    // ----- EXTENSION -----
    protected virtual void OnAttack(){ }
    protected virtual void OnMove(){ }
    
    // ----- HEALPER -----
    private Transform FindNearestTarget()
    {
        _findTimer += Time.deltaTime;
        if (_findTimer < FindSurvivorCooldown)
            return _target;
        _findTimer = 0;
        return entityManager.GetSurvivorNearsest(transform.position);
    }

    private void LookAhead(Vector3 direction)
    {
        Quaternion targetRot = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            RotateSpeed * Time.deltaTime
        );
    }

    private void Initalize()
    {
        Speed = config.Speed;
        RotateSpeed = config.RotateSpeed;
        Damage = config.Damage;
        AttackRange = config.AttackRange;
        FindSurvivorCooldown = config.FindSurvivorCooldown;
        AttackSurvivorCooldown = config.AttackSurvivorCooldown;
        
        _target = null;
        _attackTimer = 0f;
        _findTimer = 0f;
        
        attackPoint.localPosition = Vector3.forward * AttackRange / 2;
    }
    
    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
    }
}
