using UnityEngine;

public interface IAbility
{
    public AbilityType AbilityType { get; }
    public bool IsReady { get; }
    public float Cooldown { get; }
    public void Active();
        
}
