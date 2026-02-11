using System;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] private float speed;
    private float _lifeTime;

    public void Init(float _lifeTime)
    {
        this._lifeTime = _lifeTime;
        OnDespawn(_lifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * (Time.deltaTime * speed);
    }
}
