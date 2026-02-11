using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Survivor : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] private float hp = 100f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fireRate;
    private float _currentHp = 100f;
    
    [Header("REFERENCES")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Weapon weapon;
    
    public float Hp => hp;
    public float MoveSpeed => moveSpeed;
    public float FireRate => fireRate;
    

    private void Start()
    {
        _currentHp = hp;
        weapon.Init(fireRate);
    }

    public void TakeDamage(float amount)
    {
        _currentHp -= amount;
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        _currentHp = Mathf.Min(_currentHp + amount, Hp);
    }

    public void Die()
    {
        //TODO : Die
    }

    public void MoveHorizontal(float direction)
    {
        controller.Move(Vector3.right * (direction * moveSpeed * Time.deltaTime));
    }
}
