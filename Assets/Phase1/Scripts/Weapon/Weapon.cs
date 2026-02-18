using System;
using System.Collections.Generic;
using Phase1;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private List<MonoBehaviour> weaponObject = new List<MonoBehaviour>();
    private Dictionary<WeaponType, IWeapon> _weapons = new Dictionary<WeaponType, IWeapon>();
    private IWeapon _currentWeapon;

    private void Awake()
    {
        foreach (var comp in weaponObject)
        {
            if (comp is IWeapon weapon)
            {
                _weapons.Add(weapon.Type, weapon);
            }
        }
    }

    private void Start()
    {
        EquipWeapon(weaponType);
    }

    private void Update()
    {
        Fire();
    }

    public void EquipWeapon(WeaponType type)
    {
        weaponType = type;
        ChangeWeapon();
    }
    private void Fire()
    {
        if (_currentWeapon != null && _currentWeapon.CanAttack)
        {
            _currentWeapon.Attack();
        }
    }

    [ContextMenu("ChangeWeapon")]
    private void ChangeWeapon()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Unequip();
        }
        _currentWeapon = _weapons[weaponType];
        _currentWeapon.Equip();
    }
}
