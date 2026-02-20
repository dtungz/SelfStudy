using System;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile, IHitable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    private float damage;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }

    public void Init(float damage, Vector3 direction)
    {
        this.damage = damage;
        rb.linearVelocity = direction * speed;
    }

    public void Hit(Vector3 hitPoint, Vector3 hitDirection)
    {
        throw new NotImplementedException();
    }

    public void Hit(HitPayload payload)
    {
        rb.AddForceAtPosition(payload.Direction * payload.HitForce, payload.HitPoint, ForceMode.Impulse);
    }
}
