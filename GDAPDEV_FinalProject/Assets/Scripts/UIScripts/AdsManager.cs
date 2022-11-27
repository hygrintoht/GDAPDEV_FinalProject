using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameID;
    [SerializeField] string _iosGameID;
    [SerializeField] bool _testMode = true;

    private void Awake()
    {
        InitializedAds();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void InitializedAds()
    {
        string _gameID = (Application.platform == RuntimePlatform.IPhonePlayer) ?
            _iosGameID : _androidGameID; 

        Advertisement.Initialize(_gameID, _testMode, this);
        Debug.Log("Initializing ads system: " + _gameID);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization failed: {error.ToString()} - {message}");
    }





}
