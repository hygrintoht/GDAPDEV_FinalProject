using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadMultiple : MonoBehaviour
{
    public List<string> keys = new List<string>();
    AsyncOperationHandle<IList<GameObject>> loadHandle;

    private void Start()
    {
        loadHandle = Addressables.LoadAssetsAsync<GameObject>(
            keys,
            Loaded,
            Addressables.MergeMode.Union,
            false);
    }

    private void Loaded(GameObject addressableObj)
    {
        Instantiate<GameObject>(
            addressableObj,
            transform.position,
            Quaternion.identity,
            transform);
    }

    private void Handle_Completed(AsyncOperationHandle<IList<GameObject>> operation) 
    {
        if (operation.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Assets failed to load");
        }
    }

    private void OnDestroy()
    {
        Addressables.Release(loadHandle);
    }

}
