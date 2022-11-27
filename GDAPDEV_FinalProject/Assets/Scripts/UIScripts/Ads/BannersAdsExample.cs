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


    // Start is called before the first frame update
    void Start()
    {
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;

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

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(_adUnitId, options);
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


