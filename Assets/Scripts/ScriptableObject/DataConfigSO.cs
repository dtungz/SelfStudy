using UnityEngine;

[CreateAssetMenu(fileName = "SO_Solider", menuName = "ScriptableObject/SO_Solider")]
public class DataConfigSO : ScriptableObject
{
    [Header("CONFIG")]
    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fireRate;
    
    public int Hp => hp;
    public float MoveSpeed => moveSpeed;
    public float FireRate => fireRate;
}
