using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private List<MonoBehaviour> abilityList;
    
    private InputAction _changeAbilityInput;
    private InputAction _activeAction;
    
    private List<AbilityType> _abilitieType;
    private List<IAbility> _abilities;
    private IAbility _currentAbility;
    
    private float _changeAction;
    private int _indexAbility;

    private void Awake()
    {
        _abilitieType = new List<AbilityType>();
        _abilities = new List<IAbility>();
        _changeAbilityInput = inputManager.AbilityAction;
        _activeAction = inputManager.ActiveAction;
    }
    

    private void Start()
    {
        _indexAbility = 0;
        foreach (var ability in abilityList)
        {
            if (ability is IAbility abi)
            {
                _abilities.Add(abi);
            }
        }
    }

    private void Update()
    {
        ChangeAbility();
        ActiveAbility();
    }
    
    // ----- PUBLIC METHODS -----
    public void AddAbility(AbilityType ability)
    {
        if (_abilitieType.Contains(ability))
        {
            return;
        }
        _abilitieType.Add(ability);
        if (_abilitieType.Count == 1)
        {
            _currentAbility = FindAbility(ability);
            _indexAbility = 0;
        }
    }

    public void RemoveAbility(AbilityType ability)
    {
        _abilitieType.Remove(ability);
        if (_abilitieType.Count == 0)
        {
            _currentAbility = null;
        }
    }
    
    
    // ----- HEALPER -----
    private void ChangeAbility()
    {
        if (_abilitieType.Count == 0)
            return;
        if (_changeAbilityInput.WasPressedThisFrame())
        {
            int direction = Mathf.RoundToInt(_changeAbilityInput.ReadValue<float>());
            _indexAbility += direction;

            if (_indexAbility >= _abilitieType.Count)
                _indexAbility = 0;

            if (_indexAbility < 0)
                _indexAbility = _abilitieType.Count - 1;

            _currentAbility = FindAbility(_abilitieType[_indexAbility]);
        }
    }

    private void ActiveAbility()
    {
        if (_activeAction.WasPressedThisFrame())
        {
            Debug.Log("Active Ability");
            if (_currentAbility == null || _abilitieType.Count == 0)
            {
                // if (_currentAbility == null)
                // {
                //     Debug.Log("No Ability Selected");
                // }
                // else
                // {
                //     Debug.Log("No Ability in Range");
                // }
                return;
            }
            // Debug.Log("Active succes");
            _currentAbility.Active();
        }
    }

    private IAbility FindAbility(AbilityType abilityType)
    {
        for (int i = 0; i < _abilities.Count; i++)
        {
            if (_abilities[i].AbilityType == abilityType)
            {
                return _abilities[i];
            }
        }
        return null;
    }
}
