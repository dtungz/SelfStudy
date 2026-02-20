using System;
using UnityEngine;

public class HealAbility : MonoBehaviour, IAbility
{
    [SerializeField] private float healAmount = 20f;
    [SerializeField] private HealthComponent healthComponent;

    private void Awake()
    {
        if(healthComponent == null)
            healthComponent = GetComponent<HealthComponent>();
    }

    private float _nextUseTime;
    public AbilityType AbilityType => AbilityType.Heal;
    public float Cooldown => 5f;
    public bool IsReady => Time.time >= _nextUseTime;
    
    public void Active()
    {
        if (!IsReady)
        {
            return;
        }
        _nextUseTime = Time.time + Cooldown;
        healthComponent.Heal(healAmount);
    }
}
