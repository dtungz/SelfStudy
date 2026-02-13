using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private List<Transform> survivors = new List<Transform>();
    [SerializeField] private float findDistance;

    public Transform GetSurvivorNearsest(Vector3 position)
    {
        if (survivors.Count == 0)
        {
            return null;
        }

        Transform target = null;
        for (int i = 0; i < survivors.Count; i++)
        {
            if (target == null || Vector3.Distance(position, survivors[i].position) <
                Vector3.Distance(position, target.position))
            {
                if (Vector3.Distance(position, survivors[i].position) <= findDistance)
                {
                    target = survivors[i];
                }
            }
        }
        return target;
    }

    public void AddSurvivor(Transform survivor)
    {
        survivors.Add(survivor);
    }

    public void RemoveSurvivor(Transform survivor)
    {
        survivors.Remove(survivor);
    }
}
