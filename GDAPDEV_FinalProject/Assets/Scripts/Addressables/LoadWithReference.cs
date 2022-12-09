using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadWithReference : MonoBehaviour
{
    public AssetReference reference;

    private void Awake()
    {
        AsyncOperationHandle handle = reference.LoadAssetAsync<GameObject>();
        handle.Completed += Handle_Completed;
    }

    private void Handle_Completed(AsyncOperationHandle obj) 
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(reference.Asset, transform);
        }
        else 
        {
            Debug.LogError($"Asset Reference {reference.RuntimeKey} failed to load");
        }
    }

    private void OnDestroy()
    {
        reference.ReleaseAsset();
    }

}
