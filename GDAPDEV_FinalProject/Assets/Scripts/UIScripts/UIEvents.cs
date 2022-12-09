using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIEvents : MonoBehaviour
{
    [Header("Tab Holder")]
    [SerializeField] Image playTab;
    [SerializeField] Image loadoutTab;
    [SerializeField] Image configTab;
    [SerializeField] Image leaderTab;
    [SerializeField] TextMeshProUGUI currencyTxt;

    private Image activeTab;
    private int currencyAvail;

    [Header("Section Holder")]
    //Parameter Handler
    [SerializeField] GameObject playSection;
    [SerializeField] GameObject loadoutSection;
    [SerializeField] GameObject configSection;
    [SerializeField] GameObject leaderSection;

    private GameObject currActiveSection;
    //Color for Tab
    Color tabOrigColor;
    Color pressColor;


    [Header("Upgrade Holder")]
    [SerializeField] GameObject barPrefab;
    [SerializeField] private GameObject upgrade_1DataPanel; //try to experiment
    [SerializeField] private GameObject upgrade_2DataPanel;
    [SerializeField] private GameObject upgrade_3DataPanel;
    [SerializeField] private GameObject upgrade_4DataPanel;
    [SerializeField] private GameObject upgrade_5DataPanel;

    [SerializeField] UpgradeEvent upgradeData;

    private List<GameObject> panelHolder;
    private List<GameObject> barHolder;

    //Color for UpgradeBar
    Color emptyUpColor;
    Color filledUpColor;


    //[Header("Confirm Tab Holder")]
    //[SerializeField] GameObject confirmSection_1; //Confirm Tab: For Adding Currency


    private void Awake()
    {
        //Update the Current Status of each part   
    }

    // Start is called before the first frame update
    void Start()
    {
        //Pre Start
        barHolder = new List<GameObject>();

       //Starting Tab
        currActiveSection = playSection;
        playSection.SetActive(true);
        activeTab = playTab;

        //Color Handling
        InitColor();

        //Currency Handling
        InitCurrency();

    //Handle the upgrade handler

        //compress the panel holder for easier organization
        CompressPanel(); ;

        //init the prefab
        InitUpgrade();

        
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
        currActiveSection.SetActive(false);
        playSection.SetActive(true);
        currActiveSection = playSection;

        //Color Handling
        activeTab.color = tabOrigColor;
        playTab.color = pressColor;
        activeTab = playTab;

    }

    public void OnPressLoadoutTab()
    {
        //activate the tab and deactivate the other tab
        currActiveSection.SetActive(false);
        loadoutSection.SetActive(true);
        currActiveSection = loadoutSection;

        //Color Handling
        activeTab.color = tabOrigColor;
        loadoutTab.color = pressColor;
        activeTab = loadoutTab;
    }

    public void OnPressConfigTab()
    {
        //activate the tab and deactivate the other tab
        currActiveSection.SetActive(false);
        configSection.SetActive(true);
        currActiveSection = configSection;

        //Color Handling
        activeTab.color = tabOrigColor;
        configTab.color = pressColor;
        activeTab = configTab;
    }

    public void OnPressLeaderBoardTab()
    {
        //activate the tab and deactivate the other tab
        currActiveSection.SetActive(false);
        leaderSection.SetActive(true);
        currActiveSection = leaderSection;

        //Color Handling
        activeTab.color = tabOrigColor;
        leaderTab.color = pressColor;
        activeTab = leaderTab;
    }

    // ===== Level Selector Tab ====== //
    public void OnLevelSelect_1()
    {
        //Switch Scene from one to another
        SceneManager.LoadScene("Assets/Scenes/Updated/Ocean_DUPLICATE.unity");
    }

    public void OnLevelSelect_2()
    {
        //Switch Scene from one to another
        SceneManager.LoadScene("Assets/Scenes/Updated/City_DUPLICATE.unity");
    }

    public void OnLevelSelect_3()
    {
        //Switch Scene from one to another
        SceneManager.LoadScene("Assets/Scenes/Updated/Cave_DUPLICATE.unity");
    }

    // === Upgrade Button Presses === //

    public void UpdateUpgradeStatus()
    {
        ResetBar();
        InitUpgrade();
        UpdateCurrency();
    }

    public void OnUpgrade_1()
    {
        upgradeData.UpdateUpgradeData(1, 1000);
        upgradeData.gameObject.SetActive(true);
        
    }

    public void OnUpgrade_2()
    {
        upgradeData.UpdateUpgradeData(2, 1000);
        upgradeData.gameObject.SetActive(true);
    }

    public void OnUpgrade_3()
    {
        upgradeData.UpdateUpgradeData(3, 1000);
        upgradeData.gameObject.SetActive(true);
    }

    public void OnUpgrade_4()
    {
        upgradeData.UpdateUpgradeData(4, 1000);
        upgradeData.gameObject.SetActive(true);
    }

    public void OnUpgrade_5()
    {
        upgradeData.UpdateUpgradeData(5, 1000);
        upgradeData.gameObject.SetActive(true);
    }

    // === Config Settings Parameters == //
    public void AddMoney()
    {
        GameData.Instance.UpdateCurrency(10000);
        int totalMoney = GameData.Instance.RetrieveCurrency();
        currencyAvail = totalMoney;

        currencyTxt.text = $"{totalMoney}";

    }

    public void ResetUpgrade()
    {
        //Upgrade Data
        GameData.Instance.ResetData();
        ResetBar();
        InitUpgrade();

        //Currency
        UpdateCurrency();

    }



    //private process
    private void InitColor()
    {
        //For Tab Color Handling
        tabOrigColor = new Color();
        tabOrigColor = playTab.GetComponent<Image>().color;
        Color tabColor = new Color(tabOrigColor.r + 0.15f, tabOrigColor.g + 0.15f,
            tabOrigColor.b + 0.15f, tabOrigColor.a); //Custom Color

        pressColor = tabColor;
        activeTab.color = pressColor;


        //Upgrade Color Handling
        emptyUpColor = barPrefab.GetComponent<Image>().color;
        Color upgradeColor = new Color();
        upgradeColor = Color.red;

        filledUpColor = upgradeColor;


    }


    private void CompressPanel()
    {
        panelHolder = new List<GameObject>();
        panelHolder.Add(upgrade_1DataPanel);
        panelHolder.Add(upgrade_2DataPanel);
        panelHolder.Add(upgrade_3DataPanel);
        panelHolder.Add(upgrade_4DataPanel);
        panelHolder.Add(upgrade_5DataPanel);
    }

    private void InitUpgrade()
    {
        List<int> maxUpgradeData = GameData.Instance.retrieveMaxData();
        List<int> currUpgradeData = GameData.Instance.retrieveCurrentData();

        for (int i = 0; i < panelHolder.Count; i++) //change the max later
        {
            for (int j = 0; j < maxUpgradeData[i]; j++)
            {
                GameObject bar = Instantiate(barPrefab);
                bar.transform.SetParent(panelHolder[i].transform, false);

                //change color if already upgraded
                if(j >= currUpgradeData[i])
                {
                 
                }

                else
                {
                    Image barCopy = bar.GetComponent<Image>();
                    barCopy.color = filledUpColor;
                }

                barHolder.Add(bar);
            }
        }

        //GameData.Instance.printData();
    }

    private void ResetBar()
    {
       foreach (GameObject b in barHolder)
        {
            GameObject.Destroy(b);
        }

        barHolder.Clear();
    }

    private void InitCurrency()
    {
        int totalMoney = GameData.Instance.RetrieveCurrency();
        currencyAvail = totalMoney;

        currencyTxt.text = $"{totalMoney}";
    }

    private void UpdateCurrency()
    {
        int totalMoney = GameData.Instance.RetrieveCurrency();
        currencyAvail = totalMoney;

        currencyTxt.text = $"{totalMoney}";
    }

}
