using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIEventsExtra : MonoBehaviour
{

    [Header("Confirm Section")]
    [SerializeField] private GameObject confirmSection_1; //Confirm Tab: For Adding Currency
    [SerializeField] private GameObject confirmSection_Upgrades;

    [Header("LeaderBoardHolder")]
    [SerializeField] private GameObject Level_1Section;
    [SerializeField] private GameObject Level_2Section;
    [SerializeField] private GameObject Level_3Section;

    private GameObject activeLeaderTab;

    //Color Handling
    [Header("LeaderBoardTabs")]
    [SerializeField] Image Level_1Tab;
    [SerializeField] Image Level_2Tab;
    [SerializeField] Image Level_3Tab;

    private Color origColor;
    private Color pressedColor;
    private Image activeImage;

    [Header("LeaderBoardSpawnLoc")]
    [SerializeField] private GameObject Level_1DataHolder;
    [SerializeField] private GameObject Level_2DataHolder;
    [SerializeField] private GameObject Level_3DataHolder;

    [Header("Info Holder")]
    [SerializeField] private WebAPI referenceWebAPI;
    [SerializeField] private GameObject prefabData;
    [SerializeField] private int counter = 0;


    private List<GameObject> Level_1DataPanelList;
    private List<GameObject> Level_2DataPanelList;
    private List<GameObject> Level_3DataPanelList;

    private List<Dictionary<string, dynamic>> Level_1DataList;
    private List<Dictionary<string, dynamic>> Level_2DataList;
    private List<Dictionary<string, dynamic>> Level_3DataList;

    private void Start()
    {

        //Color Handling
        InitColor();

        //Set all Gameobject Reference to false

        //Confirm Section
        confirmSection_1.SetActive(false);


        //LeaderBoardSection
        activeLeaderTab = Level_1Section;
        activeLeaderTab.SetActive(true);

        //Setup the container
        SetupContainer();

    }

    //============== Confirm Section ==============//



    public void OnEnableConfirmSection_1()
    {
        confirmSection_1.SetActive(true);
    }

    public void OnDisableConfirmSection_1()
    {
        confirmSection_1.SetActive(false);
    }


    // =============== LeaderBoards Section ===============//

    /*ON Enable Section*/
    public void OnEnable_LeaderboardLevel_1()
    {
        // Color Handling
        activeImage.color = origColor;
        Level_1Tab.color = pressedColor;
        activeImage = Level_1Tab;

        activeLeaderTab.SetActive(false);
        activeLeaderTab = Level_1Section;
        activeLeaderTab.SetActive(true);
    }

    public void OnEnable_LeaderboardLevel_2()
    {
        activeImage.color = origColor;
        Level_2Tab.color = pressedColor;
        activeImage = Level_2Tab;

        activeLeaderTab.SetActive(false);
        activeLeaderTab = Level_2Section;
        activeLeaderTab.SetActive(true);
    }

    public void OnEnable_LeaderboardLevel_3()
    {
        activeImage.color = origColor;
        Level_3Tab.color = pressedColor;
        activeImage = Level_3Tab;

        activeLeaderTab.SetActive(false);
        activeLeaderTab = Level_3Section;
        activeLeaderTab.SetActive(true);
    }


    //Debug Purpose
    public void testSpawn()
    {

        for (counter = 1; counter < 10; counter++)
        {

            GameObject leaderGameData = GameObject.Instantiate(prefabData);
            leaderGameData.transform.SetParent(Level_1DataHolder.transform, false);

            leaderGameData.GetComponent<LeaderboardData>().setData(counter, "sample", 0);
            //Level_1DataPanelList.Add(leaderGameData);

            Debug.Log($"Counter: {counter}");
        }

    }

    public void SetupContainer()
    {
        //init Game Object
        Level_1DataPanelList = new List<GameObject>();
        Level_2DataPanelList = new List<GameObject>();
        Level_3DataPanelList = new List<GameObject>();

        //Load All Data
        Level_1DataList = new List<Dictionary<string, dynamic>>();
        Level_2DataList = new List<Dictionary<string, dynamic>>();
        Level_3DataList = new List<Dictionary<string, dynamic>>();
    }


    /*Transfering Data Section*/
    public void InitData()
    {
        ResetAllData();


        //Load All Data
        Level_1DataList = referenceWebAPI.ReturnLevel1_SetScores();
        Level_2DataList = referenceWebAPI.ReturnLevel2_SetScores();
        Level_3DataList = referenceWebAPI.ReturnLevel3_SetScores();

        //Create a prefab for everysingle one

        //int counter = 0;

        //Debug.Log($"DataList Count: {Level_1DataList.Count}");

        //Level 1
        InsertList(Level_1DataHolder, Level_1DataPanelList, Level_1DataList);

        //Level 2
        InsertList(Level_2DataHolder, Level_2DataPanelList, Level_2DataList);

        //Level 3
        InsertList(Level_3DataHolder, Level_3DataPanelList, Level_3DataList);

    }


    public void ResetAllData()
    {
        //Delete them manually per level Data

        //Level1
        Debug.Log($"Game container size: {Level_1DataPanelList.Count} ");
        if (Level_1DataPanelList.Count >= 1)
        {
            foreach (GameObject gameData in Level_1DataPanelList)
            {
                GameObject.Destroy(gameData);
            }
            Level_1DataPanelList.Clear();
        }

        //Level2
        if (Level_2DataPanelList.Count >= 1)
        {
            foreach (GameObject gameData in Level_2DataPanelList)
            {
                GameObject.Destroy(gameData);
            }
            Level_2DataPanelList.Clear();
        }
        //Level3


        if (Level_3DataPanelList.Count >= 1)
        {
            foreach (GameObject gameData in Level_3DataPanelList)
            {
                GameObject.Destroy(gameData);
            }
            Level_3DataPanelList.Clear();
        }

    }


    //Dynamic Calling
    public void InsertList(GameObject PanelLocList, List<GameObject> Level_DataPanelList,  List<Dictionary<string, dynamic>> scoreList)
    {
        //Optional: create an if statement if have wifi

        //Check if the data exist
        
        if (Level_DataPanelList.Count < 10) {

            //Determine if null the data or no items
            if (scoreList.Count == 0 || scoreList == null)
            {
                Debug.Log("Eempty Data || Check your internet Connection");
                NoInternetIndicator(PanelLocList, Level_DataPanelList);
            }

            else
            {
                counter = 0; // assuming is empty

                

                Debug.Log($"Count {scoreList.Count}");
                for (int i = 0; (i < scoreList.Count) && i < 10; i++)
                //foreach (Dictionary<string, dynamic> leaderData in Level_1DataList)
                {
                    //(Dictionary<string, dynamic> leaderData in Level_1DataList)
                    Dictionary<string, dynamic> leaderData = scoreList[i];


                    //Data Parsing
                    counter++;
                    string Name = leaderData["user_name"];
                    int score = (int)leaderData["score"];


                    //Prefab Handling
                    GameObject leaderGameData = GameObject.Instantiate(prefabData);
                    leaderGameData.transform.SetParent(PanelLocList.transform, false);

                    leaderGameData.GetComponent<LeaderboardData>().setData(counter, Name, score);
                    Level_DataPanelList.Add(leaderGameData);

                    //Debug.Log(counter);
                }

                //Contingency Plan if list has less than 10 items
                while (counter < 10)
                {
                    counter++;
                    GameObject leaderGameData = GameObject.Instantiate(prefabData);
                    leaderGameData.transform.SetParent(PanelLocList.transform, false);

                    leaderGameData.GetComponent<LeaderboardData>().setData(counter, "Filler", 0);
                    Level_DataPanelList.Add(leaderGameData);

                    Debug.Log(counter);
                }
            }
        }

        else
        {
            Debug.Log("Data has already been filled");
        }

    }



    //No Wifi Info
    private void NoInternetIndicator (GameObject PanelLocList, List<GameObject>PanelList){

        string message = "NO INTERNET OR NO DATA, TRY AGAIN LATER";

            GameObject leaderGameData = GameObject.Instantiate(prefabData);
            leaderGameData.transform.SetParent(PanelLocList.transform, false);

            leaderGameData.GetComponent<LeaderboardData>().setData(0, message, 0);
            PanelList.Add(leaderGameData);
        
    }


    //Color Handling
    private void InitColor()
    {

        //For Tab Color Handling
        origColor = new Color();
        origColor = Level_1Tab.GetComponent<Image>().color;
        Color tabColor = new Color(origColor.r + 0.15f, origColor.g + 0.15f,
            origColor.b + 0.15f, origColor.a); //Custom Color

        pressedColor = tabColor;
        Level_1Tab.color = pressedColor;
        activeImage = Level_1Tab;
    }


}
