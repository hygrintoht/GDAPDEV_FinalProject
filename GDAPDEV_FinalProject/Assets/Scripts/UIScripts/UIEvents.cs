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
    [SerializeField] TextMeshProUGUI currencyTxt;

    private Image activeTab;
    private int currencyAvail;

    [Header("Section Holder")]
    //Parameter Handler
    [SerializeField] GameObject playSection;
    [SerializeField] GameObject loadoutSection;
    [SerializeField] GameObject configSection;
    // [SerializeField] GameObject leaderTab //Enable it Later

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

    // ===== Level Selector Tab ====== //
    public void OnLevelSelect_1()
    {
        //Switch Scene from one to another
        SceneManager.LoadScene(2);
    }

    public void OnLevelSelect_2()
    {
        //Switch Scene from one to another
    }

    public void OnLevelSelect_3()
    {
        //Switch Scene from one to another
    }

    // === Upgrade Button Presses === //
    public void OnUpgrade_1()
    {
        //if Success
        if (currencyAvail > 1000)
        {


            ResetBar();
            GameData.Instance.IncrementData(1);
            InitUpgrade();

            GameData.Instance.UpdateCurrency(-1000);
            UpdateCurrency();
        }

    }

    public void OnUpgrade_2()
    {
        if (currencyAvail > 1000)
        {
            ResetBar();
            GameData.Instance.IncrementData(2);
            InitUpgrade();

            GameData.Instance.UpdateCurrency(-1000);
            UpdateCurrency();
        }
    }

    public void OnUpgrade_3()
    {
        if (currencyAvail > 1000)
        {
            ResetBar();
            GameData.Instance.IncrementData(3);
            InitUpgrade();

            GameData.Instance.UpdateCurrency(-1000);
            UpdateCurrency();
        }
    }

    public void OnUpgrade_4()
    {
        if (currencyAvail > 1000)
        {
            ResetBar();
            GameData.Instance.IncrementData(4);
            InitUpgrade();

            GameData.Instance.UpdateCurrency(-1000);
            UpdateCurrency();
        }
    }

    public void OnUpgrade_5()
    {
        if (currencyAvail > 1000)
        {
            ResetBar();
            GameData.Instance.IncrementData(5);
            InitUpgrade();

            GameData.Instance.UpdateCurrency(-1000);
            UpdateCurrency();
        }
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
