using System;
using Interfaces;
using Phase1;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] private float speed;
    private float _damage;
    private FactionType _ownerType;

    public void Init(float _lifeTime, float damage,  FactionType ownerType)
    {
        CancelInvoke();
        _ownerType = ownerType;
        _damage = damage;
        OnDespawn(_lifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * (Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponent<IDamageable>();
        if (target != null && target.Faction != _ownerType)
        {
            target.TakeDamage(_damage);
            CancelInvoke();
            OnDespawn(0);
        }
    }
}
