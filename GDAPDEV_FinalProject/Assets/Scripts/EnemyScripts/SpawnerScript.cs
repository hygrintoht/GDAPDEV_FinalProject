using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] EnemyBehavior enemyPrefab;
    [SerializeField] EnemyBehavior.Direction spawnerDirection;

    ObjectPool<EnemyBehavior> pool;

    float spawnTimer;
    Vector3 center;
    Vector3 size;

    void Start()
    {
        pool = new ObjectPool<EnemyBehavior>(
            () => { return Instantiate(enemyPrefab); },
            enemy => { enemy.gameObject.SetActive(true); },
            enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); },
            false,
            15,
            30
            );

        spawnTimer = Random.Range(5.0f, 15.0f);
        center = gameObject.transform.position;
        size = gameObject.transform.localScale;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            EnemyBehavior enemy = pool.Get();
            enemy.SetEnemyParams();
            enemy.transform.position = RandomPositionInSpawner();
            enemy.MoveToDirection(spawnerDirection);
            spawnTimer = Random.Range(5.0f, 15.0f);
        }
    }

    Vector3 RandomPositionInSpawner()
    {
        return center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
    }
}
