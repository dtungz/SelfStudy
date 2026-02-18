using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static Dictionary<PoolType, Pool> Pools = new Dictionary<PoolType, Pool>();

    public static void OnInit(GameUnit prefab, int amount, Transform root = null)
    {
        if (prefab != null && !Pools.ContainsKey(prefab.poolType))
        {
            Pool pool = new Pool(prefab, amount, root);
            pool.Init();
            Pools.Add(prefab.poolType, pool);
        }
    }

    public static T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if (!Pools.ContainsKey(poolType))
        {
            Debug.LogError("Pool not initialized: " + poolType);
            return null;
        }

        return Pools[poolType].Spawn(pos, rot) as T;
    }

    public static void Despawn(GameUnit obj)
    {
        if (obj != null && Pools.ContainsKey(obj.poolType))
        {
            Pools[obj.poolType].Despawn(obj);
        }
    }

    public static void CollectAll()
    {
        foreach (var pool in Pools.Values)
        {
            pool.Collect();
        }
    }
}
