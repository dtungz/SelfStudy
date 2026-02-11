using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon")] 
    [SerializeField] private WeaponType currentWeaponType;
    [SerializeField] private List<WeaponBase>  weapons = new List<WeaponBase>();
    private Dictionary<WeaponType, WeaponBase> _weaponsMap;
    private WeaponBase _currentWeapon;
    private bool _canFire;
    private float _fireRate;
    private float _currentFireRate;

    private void Awake()
    {
        _weaponsMap = new Dictionary<WeaponType, WeaponBase>();
        foreach (var weapon in weapons)
        {
            _weaponsMap.Add(weapon.WeaponType, weapon);
        }
    }

    private void Start()
    {
        _canFire = true;
        ChangeWeapon(WeaponType.Piston);
        _currentFireRate = 0f;
    }

    private void Update()
    {
        if (_canFire)
        {
            
            if (_currentFireRate >= _fireRate)
            {
                Fire();
                _currentFireRate = 0f;
            }
            else
            {
                _currentFireRate += Time.deltaTime;
            }
        }
    }

    [ContextMenu("ChangeWeapon")]
    public void ChangeWeapon(WeaponType weaponType)
    {
        if (_weaponsMap.ContainsKey(weaponType))
        {
            _currentWeapon = _weaponsMap[weaponType];
        }
    }

    private void Fire()
    {
        _currentWeapon.Fire();
    }

    public void Init(float _fireRate)
    {
        this._fireRate = _fireRate;
    }
}
