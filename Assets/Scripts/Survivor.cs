using Interfaces;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Survivor : MonoBehaviour, IDamageable
{
    [Header("CONFIG")]
    [SerializeField] private float moveSpeed;
    
    [Header("REFERENCES")]
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private EntityManager entityManager;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Weapon weapon;
    
    public float MoveSpeed => moveSpeed;

    private void OnEnable()
    {
        entityManager.AddSurvivor(transform);
    }

    private void OnDisable()
    {
        entityManager.RemoveSurvivor(transform);
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
}
