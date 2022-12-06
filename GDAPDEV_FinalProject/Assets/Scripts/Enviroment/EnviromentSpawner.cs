using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnviromentSpawner : MonoBehaviour
{
    [SerializeField] List<EnviromentObject> enviromentObjectPrefabs;
    [SerializeField] float minSpawnTime = 5.0f;
    [SerializeField] float maxSpawnTime = 10.0f;

    ObjectPool<EnviromentObject> pool;

    bool spawnerStatus = true;

    float spawnTimer;
    Vector3 center;
    Vector3 size;

    void Start()
    {
        pool = new ObjectPool<EnviromentObject>(
            () => { return Instantiate(RandomEnvirtomentObjectFromPrefabs()); },
            enviromentObject => { enviromentObject.gameObject.SetActive(true); },
            enviromentObject => { enviromentObject.gameObject.SetActive(false); },
            enviromentObject => { Destroy(enviromentObject.gameObject); },
            false,
            15,
            30
            );

        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
        center = gameObject.transform.position;
        size = gameObject.transform.localScale;
    }

    void Update()
    {
        if (spawnerStatus)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                EnviromentObject enviromentObject = pool.Get();
                enviromentObject.transform.position = RandomPositionInSpawner();
                enviromentObject.transform.rotation = transform.rotation;
                enviromentObject.Init(KillEnviromentObject);
                spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
            }
        }
    }
    Vector3 RandomPositionInSpawner()
    {
        return center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
    }

    private void KillEnviromentObject(EnviromentObject enviromentObject) 
    { 
        pool.Release(enviromentObject); 
    }

    private EnviromentObject RandomEnvirtomentObjectFromPrefabs()
    {
        return enviromentObjectPrefabs[Random.Range(0, enviromentObjectPrefabs.Count)];
    }
}
