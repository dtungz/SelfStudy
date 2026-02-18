using System;
using System.Collections.Generic;
using Phase1;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private Survivor survivor;
    [SerializeField] private List<EnemyBase> enemyPrefabs = new List<EnemyBase>();
    [SerializeField] private float floorWidth;
    [SerializeField] private int enemyNumberSpawn;
    [SerializeField] private Transform enemySpawnPoint;
    private List<EnemyBase> _enemies = new List<EnemyBase>();

    private void OnEnable()
    {
        survivor.AddFindEnemyEvent(FindNearestEnemy);
    }

    private void OnDisable()
    {
        survivor.RemoveFindEnemyEvent(FindNearestEnemy);
    }

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
            enemy.AddGetTargetEvent(SetEnemyTarget);
            _enemies.Add(enemy);
            enemy.AddOnDieEvent(RemoveEnemy);
        }
    }

    public void SetEnemyTarget(EnemyBase enemy)
    {
        enemy.SetTarget(survivor.transform);
    }

    public void RemoveEnemy(EnemyBase enemy)
    {
        _enemies.Remove(enemy);
        if (_enemies.Count == 0)
        {
            Invoke("SpawnEnemy", 3f);
        }
    }

    public void FindNearestEnemy()
    {
        if (_enemies.Count == 0)
        {
            survivor.SetTarget(null);
        }

        Transform target = null;
        Vector3 position = survivor.GetPosition();
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (target == null || Vector3.Distance(position, target.position) >
                Vector3.Distance(position, _enemies[i].GetPosition()))
            {
                target = _enemies[i].transform;
            }
        }
        survivor.SetTarget(target);
    }
}
