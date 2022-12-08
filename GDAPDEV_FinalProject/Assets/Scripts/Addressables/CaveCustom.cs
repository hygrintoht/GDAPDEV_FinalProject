using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CaveCustom : MonoBehaviour
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
            var ceiling = Instantiate(reference.Asset, transform);
            ceiling.GetComponent<Transform>().Translate(new Vector3(0, 30, 0));
            ceiling.GetComponent<Transform>().Rotate(new Vector3(0, 0, 180f));


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
