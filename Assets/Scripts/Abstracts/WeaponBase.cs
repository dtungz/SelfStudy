using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponType _weaponType;
    public WeaponType WeaponType => _weaponType;
    
    abstract public void Fire();

}
