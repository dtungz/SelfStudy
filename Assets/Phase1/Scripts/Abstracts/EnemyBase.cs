using System;
using Interfaces;
using UnityEngine;
using Phase1;

namespace Phase1
{
    public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    public enum ZombieType{Basic, Fast, Tank}
    
    [Header("TYPE")]
    [SerializeField] protected ZombieType zombieType;
    
    
    [Header("REFERENCES")]
    [SerializeField] protected HealthComponent healthComponent;
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
    
    public event Action <EnemyBase> getTargetEvent;
    public event Action <EnemyBase> dieEvent;
    public FactionType Faction => FactionType.Enemy;
    

    private void Awake()
    {
        Initalize();
    }

    private void Start()
    {
        healthComponent.AddDieEvent(OnDie);
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
            if (Time.time <= _findTimer)
                return;
            _findTimer = Time.time + FindSurvivorCooldown;
            getTargetEvent?.Invoke(this);
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

    private void OnDie()
    {
        dieEvent?.Invoke(this);
    }
    
    //----- PUBLIC METHODS -----
    
    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
    }

    public void SetTarget(Transform _target)
    {
        this._target =  _target;        
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void AddOnDieEvent(Action<EnemyBase> dieAction)
    {
        dieEvent += dieAction;
    }

    public void AddGetTargetEvent(Action<EnemyBase> getTargetAction)
    {
        getTargetEvent += getTargetAction;
    }
    
}

}
