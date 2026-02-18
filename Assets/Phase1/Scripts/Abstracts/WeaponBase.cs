using Phase1;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponType _weaponType;
    [SerializeField] protected float damage;
    public WeaponType WeaponType => _weaponType;
    
    abstract public void Fire();

}
