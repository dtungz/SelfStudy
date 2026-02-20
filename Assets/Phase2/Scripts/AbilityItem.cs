using System;
using UnityEngine;

public class AbilityItem : MonoBehaviour
{
    [SerializeField] private AbilityType abilityType;

    private void OnTriggerEnter(Collider other)
    {
        PlayerAbility playerAbility = other.GetComponentInParent<PlayerAbility>();
        if (playerAbility == null)
            return;
        playerAbility.AddAbility(abilityType);
        Destroy(gameObject);
    }
}
