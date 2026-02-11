using UnityEngine;

public class GameUnit : MonoBehaviour
{
    public PoolType poolType;

    public virtual void OnDespawn(float delay)
    {
        Invoke(nameof(Despawn), delay);   
    }

    private void Despawn()
    {
        PoolManager.Despawn(this);
    }
}
