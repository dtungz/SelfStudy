using System;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] private float speed;
    private float _lifeTime;
    private float _damage;

    public void Init(float _lifeTime, float damage)
    {
        this._lifeTime = _lifeTime;
        _damage = damage;
        OnDespawn(_lifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * (Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
            OnDespawn(0);
        }
    }
}
