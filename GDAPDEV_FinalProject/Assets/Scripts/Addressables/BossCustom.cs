using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BossCustom : MonoBehaviour
{
    public AssetReference reference;
    private GameObject boss;

    public GameObject Boss
    {
        get { return boss; }
    }

    private void Awake()
    {
        AsyncOperationHandle handle = reference.LoadAssetAsync<GameObject>();
        handle.Completed += Handle_Completed;
    }

    private void Handle_Completed(AsyncOperationHandle obj) 
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            boss = Instantiate(reference.Asset, transform) as GameObject;
            boss.SetActive(false);
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
