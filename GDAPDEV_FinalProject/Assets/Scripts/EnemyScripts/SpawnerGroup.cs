using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGroup : MonoBehaviour
{
    [SerializeField]SpawnerScript[] spawners;

    public void ChangeSpwanersActiveStatus(bool status)
    {
        foreach (SpawnerScript spawner in spawners)
        {
            spawner.ChangeSpwanerActiveStatus(status);
        }
    }
}
