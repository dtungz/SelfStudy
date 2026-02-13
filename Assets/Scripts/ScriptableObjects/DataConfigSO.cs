using UnityEngine;

[CreateAssetMenu(fileName = "DataConfigSO", menuName = "ScriptableObjects/DataConfigSO")]
public class DataConfigSO : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float findSurvivorCooldown;
    [SerializeField] private float attackSurvivorCooldown;
    
    public float Speed => speed;
    public float RotateSpeed => rotateSpeed;
    public float Damage => damage;
    public float AttackRange => attackRange;
    public float FindSurvivorCooldown => findSurvivorCooldown;
    public float AttackSurvivorCooldown => attackSurvivorCooldown;
}
