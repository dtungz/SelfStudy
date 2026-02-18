using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    List<GameUnit> active = new List<GameUnit>();
    Queue<GameUnit> inactive = new Queue<GameUnit>();

    private Transform root;
    private GameUnit prefab;
    private int amount;
    public Pool(GameUnit prefab, int amount, Transform root)
    {
        this.root = root;
        this.prefab = prefab;
        this.amount = amount;
    }

    public void Init()
    {
        for (int i = 0; i < amount; i++)
        {
            Spawn(Vector3.zero,  Quaternion.identity);
        }
        Collect();
        
    }

    public GameUnit Spawn(Vector3 position, Quaternion rotation)
    {
        GameUnit obj;
        if (inactive.Count == 0)
        {
            obj = GameObject.Instantiate(prefab);
        }
        else
        {
            obj = inactive.Dequeue();
        }
        obj.transform.parent = root;
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        
        obj.gameObject.SetActive(true);
        
        active.Add(obj);
        return obj;
    }

    public void Despawn(GameUnit obj)
    {
        if (obj != null && obj.gameObject.activeSelf)
        {
            obj.gameObject.SetActive(false);
            inactive.Enqueue(obj);
            active.Remove(obj);
        }
    }

    public void Collect()
    {
        for (int i = active.Count - 1; i >= 0; i--)
        {
            Despawn(active[i]);
        }
    }
}
