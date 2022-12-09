using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;

public class BannersAdsExample : MonoBehaviour
{
    [SerializeField] Button _showBannerBtn;
    [SerializeField] Button _hideBannerBtn;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOsAdUnitId = "Banner_iOS";
    string _adUnitId;

    private void Awake()
    {
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitsForAdsManagerInitialized()
    {
        yield return new WaitUntil(() => Advertisement.isInitialized);
        LoadBanner();
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoad,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(_adUnitId, options);
    }

    void OnBannerLoad()
    {
        Debug.Log("Banner Loaded");
        _showBannerBtn.interactable = true;
    }

    void OnBannerError(string message)
    {
        Debug.Log($"Banner load error: {message}");
       
    }

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            showCallback = OnBannerShown,
            hideCallback = OnBannerHidden


        };

        
        Advertisement.Banner.Show(_adUnitId, options);
        OnBannerLoad();


    }

    void OnBannerClicked() { }

    void OnBannerShown()
    {
        _showBannerBtn.interactable = false;
        _hideBannerBtn.interactable = true;
    }

    void OnBannerHidden()
    {
        _showBannerBtn.interactable = true;
        _hideBannerBtn.interactable = false;
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
}


