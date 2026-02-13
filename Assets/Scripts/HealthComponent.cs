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
        //Debug.Log("Damaged by frame " + Time.frameCount + " caller: " + gameObject.name);
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        _currentHp = Mathf.Min(_currentHp + amount, health);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
