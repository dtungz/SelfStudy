using Interfaces;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Survivor : MonoBehaviour, IDamageable
{
    [Header("CONFIG")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fireRate;
    
    [Header("REFERENCES")]
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private EntityManager entityManager;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Weapon weapon;
    
    public float MoveSpeed => moveSpeed;
    public float FireRate => fireRate;

    private void OnEnable()
    {
        entityManager.AddSurvivor(transform);
    }

    private void OnDisable()
    {
        entityManager.RemoveSurvivor(transform);
    }

    private void Start()
    {
        weapon.Init(fireRate);
    }

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

    public void Knockkback(Vector3 sourceKnock)
    {
        Vector3 backDirection = transform.position - sourceKnock;
        controller.Move(backDirection.normalized * (moveSpeed * Time.deltaTime));
    }
}
