using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{


    //Parameter Handler
    [SerializeField] GameObject playTab;
    [SerializeField] GameObject loadoutTab;
    [SerializeField] GameObject configTab;
    // [SerializeField] GameObject leaderTab //Enable it Later
    private GameObject currActiveTab = null;

    //Upgrade Handler
    [SerializeField] GameObject barPrefab;
    [SerializeField] private List<GameObject> upgrade_1Data;
    [SerializeField] private List<GameObject> upgrade_2Data;
    [SerializeField] private List<GameObject> upgrade_3Data;
    [SerializeField] private List<GameObject> upgrade_4Data;
    [SerializeField] private List<GameObject> upgrade_5Data;

    //Color management


    private void Awake()
    {
        //Update the Current Status of each part
    }

    // Start is called before the first frame update
    void Start()
    {
        currActiveTab = playTab;
        playTab.SetActive(true);

        //Handle the upgrade handler
        //init the prefab

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Event Call
    //==== For Tab Control ==//
    public void OnPressPlayTab()
    {
        //activate the tab and deactivate the other tab
        currActiveTab.SetActive(false);
        playTab.SetActive(true);
        currActiveTab = playTab;

    }

    public void OnPressLoadoutTab()
    {
        //activate the tab and deactivate the other tab
        currActiveTab.SetActive(false);
        loadoutTab.SetActive(true);
        currActiveTab = loadoutTab;
    }

    public void OnPressConfigTab()
    {
        //activate the tab and deactivate the other tab
        currActiveTab.SetActive(false);
        configTab.SetActive(true);
        currActiveTab = configTab;
    }

    // ===== Level Selector Tab ====== //
    void OnLevelSelect_1()
    {
        
    }

    void OnLevelSelect_2()
    {

    }

    void OnLevelSelect_3()
    {

    }

    // === Upgrade Button Presses === //
    void OnUpgrade_1()
    {

    }

    void OnUpgrade_2()
    {

    }

    void OnUpgrade_3()
    {

    }

    void OnUpgrade_4()
    {

    }

    void OnUpgrade_5()
    {

    }

    // === Config Settings Parameters == //




    //private process
    private void initUpgrade()
    {
        List<int> maxUpgradeData = GameData.Instance.retrieveMaxData();
        List<int> currUpgradeData = GameData.Instance.retrieveCurrentData();


    }



}
