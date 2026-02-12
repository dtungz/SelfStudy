using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float health;
    private float _currentHp;

    private void Start()
    {
        _currentHp = health;
    }

    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
