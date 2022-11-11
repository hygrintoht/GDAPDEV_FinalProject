using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGroup : MonoBehaviour
{
    public static SpawnerGroup instance;

    private void InitializeSingleton()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public static SpawnerGroup GetInstance() { return instance; }

    private void Awake()
    {
        InitializeSingleton();
    }

    [SerializeField]SpawnerScript[] spawners;

    public void ChangeSpwanersActiveStatus(bool status)
    {
        foreach (SpawnerScript spawner in spawners)
        {
            spawner.ChangeSpwanerActiveStatus(status);
        }
    }
}
