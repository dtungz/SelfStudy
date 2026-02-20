using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private HealEventChannelSO healEventChannel;
    private float _currHp;

    private void Start()
    {
        _currHp = maxHp;
    }
    
    [ContextMenu("TakeDamage")]
    public void TakeDamage(float damage)
    {
        _currHp -= damage;
        if (_currHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("PlayerDie");
    }
    
    public void Heal(float value)
    {
        _currHp = Mathf.Min(_currHp + value, maxHp);
        healEventChannel.Raise(_currHp, maxHp);
    }
}
