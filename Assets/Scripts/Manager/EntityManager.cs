using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private List<Transform> survivors = new List<Transform>();
    [SerializeField] private List<EnemyBase> enemyPrefabs = new List<EnemyBase>();
    [SerializeField] private float floorWidth;
    [SerializeField] private int enemyNumberSpawn;
    [SerializeField] private Transform enemySpawnPoint;

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < enemyNumberSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Count);
            Vector3 position = new Vector3(Random.Range(-floorWidth / 2, floorWidth / 2), enemySpawnPoint.position.y,
                enemySpawnPoint.position.z);
            EnemyBase enemy = Instantiate(enemyPrefabs[randomEnemy], position , Quaternion.identity, enemySpawnPoint);
            enemy.getTarget.AddListener(SetEnemyTarget);
        }
    }
    
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
                target = survivors[i];
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

    public void SetEnemyTarget(EnemyBase enemy)
    {
        Transform target = GetSurvivorNearsest(enemy.GetPosition());
        enemy.SetTarget(target);
    }
}
