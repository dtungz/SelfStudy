using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private Transform[] survivors;
    [SerializeField] private float findDistance;

    public Transform GetSurvivorNearsest(Vector3 position)
    {
        if (survivors.Length == 0)
        {
            return null;
        }

        Transform target = null;
        for (int i = 0; i < survivors.Length; i++)
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
    
}
