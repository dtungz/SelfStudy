using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    public List<ObjectPool> pools;

    public void Awake()
    {
        for (int i = 0; i < pools.Count; i++)
        {
            PoolManager.OnInit(pools[i].prefabs, pools[i].amount, pools[i].root);
        }
    }

    [System.Serializable]
    public class ObjectPool
    {
        public Transform root = null;
        public GameUnit prefabs = null;
        public int amount = 0;
    }
}
