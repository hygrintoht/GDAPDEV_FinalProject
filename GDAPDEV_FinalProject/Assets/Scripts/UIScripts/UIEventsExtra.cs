using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

public class UIEventsExtra : MonoBehaviour
{

    [Header("Confirm Section")]
    [SerializeField] private GameObject confirmSection_1; //Confirm Tab: For Adding Currency

    [Header("LeaderBoardHolder")]
    [SerializeField] private GameObject Level_1Section;
    [SerializeField] private GameObject Level_2Section;
    [SerializeField] private GameObject Level_3Section;

    private GameObject activeLeaderTab;

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


        //Set all Gameobject Reference to false
        //Confirm Section
        confirmSection_1.SetActive(false);


        //LeaderBoardSection
        activeLeaderTab = Level_1Section;
        activeLeaderTab.SetActive(true);

        //Call the JSON File for leaderboard things
        //StartCoroutine(initData());
        //testSpawn();

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
        activeLeaderTab.SetActive(false);
        activeLeaderTab = Level_1Section;
        activeLeaderTab.SetActive(true);
    }

    public void OnEnable_LeaderboardLevel_2()
    {
        activeLeaderTab.SetActive(false);
        activeLeaderTab = Level_1Section;
        activeLeaderTab.SetActive(true);
    }

    public void OnEnable_LeaderboardLevel_3()
    {
        activeLeaderTab.SetActive(false);
        activeLeaderTab = Level_1Section;
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


    /*Transfering Data Section*/
    public void initData()
    {
        //init Game Object
        Level_1DataPanelList = new List<GameObject>();
        Level_2DataPanelList = new List<GameObject>();
        Level_3DataPanelList = new List<GameObject>();

        //Load All Data
        Level_1DataList = new List<Dictionary<string, dynamic>>();
        Level_2DataList = new List<Dictionary<string, dynamic>>();
        Level_3DataList = new List<Dictionary<string, dynamic>>();




        //Load All Data
        Level_1DataList = referenceWebAPI.ReturnLevel1_SetScores();
        //Level_2DataList = referenceWebAPI.returnLevel2_SetScores();
        //Level_3DataList = referenceWebAPI.returnLevel3_SetScores();

        //Create a prefab for everysingle one

        //int counter = 0;

        //Debug.Log($"DataList Count: {Level_1DataList.Count}");

        //Level 1
        InsertList(Level_1DataHolder, Level_1DataPanelList, Level_1DataList);

        //for (int i = 0; i <= Level_1DataList.Count; i++)
        ////foreach (Dictionary<string, dynamic> leaderData in Level_1DataList)
        //{
        //    //(Dictionary<string, dynamic> leaderData in Level_1DataList)
        //    Dictionary<string, dynamic> leaderData = Level_1DataList[i];


        //    //Data Parsing
        //    counter++;
        //    string Name = leaderData["user_name"];
        //    int score = (int)leaderData["score"];


        //    //Prefab Handling
        //    GameObject leaderGameData = GameObject.Instantiate(prefabData);
        //    leaderGameData.transform.SetParent(Level_1DataHolder.transform, false);

        //    leaderGameData.GetComponent<LeaderboardData>().setData(counter, Name, score);
        //    Level_1DataPanelList.Add(leaderGameData);

        //    Debug.Log(counter);
        //}

        //while (counter <= 10)
        //{
        //    counter++;
        //    GameObject leaderGameData = GameObject.Instantiate(prefabData);
        //    leaderGameData.transform.SetParent(Level_1DataHolder.transform, false);

        //    leaderGameData.GetComponent<LeaderboardData>().setData(counter, "Filler", 0);
        //    Level_1DataPanelList.Add(leaderGameData);

        //    Debug.Log(counter);
        //}

        //counter = 0;

        ////Level 2
        //foreach (Dictionary<string, dynamic> leaderData in Level_2DataList)
        //{

        //    //Data Parsing
        //    counter++;
        //    string Name = leaderData["user_name"];
        //    int Scores = leaderData["score"];

        //    //Prefab Handling
        //    GameObject leaderGameData = GameObject.Instantiate(prefabData);
        //    leaderGameData.transform.SetParent(Level_2DataHolder.transform, false);

        //    leaderGameData.GetComponent<LeaderboardData>().setData(counter, Name, Scores);
        //    Level_2DataPanelList.Add(leaderGameData);

        //}

        //while (counter < 10)
        //{
        //    counter++;
        //    GameObject leaderGameData = GameObject.Instantiate(prefabData);
        //    leaderGameData.transform.SetParent(Level_2DataHolder.transform, false);

        //    leaderGameData.GetComponent<LeaderboardData>().setData(counter, null, 0);
        //    Level_2DataPanelList.Add(leaderGameData);
        //}

        //counter = 0;

        ////Level 3
        //foreach (Dictionary<string, dynamic> leaderData in Level_3DataList)
        //{

        //    //Data Parsing
        //    counter++;
        //    string Name = leaderData["user_name"];
        //    int Scores = leaderData["score"];

        //    //Prefab Handling
        //    GameObject leaderGameData = GameObject.Instantiate(prefabData);
        //    leaderGameData.transform.SetParent(Level_3DataHolder.transform, false);

        //    leaderGameData.GetComponent<LeaderboardData>().setData(counter, Name, Scores);
        //    Level_3DataPanelList.Add(leaderGameData);
        //}

        //while (counter < 10)
        //{
        //    counter++;
        //    GameObject leaderGameData = GameObject.Instantiate(prefabData);
        //    leaderGameData.transform.SetParent(Level_3DataHolder.transform, false);

        //    leaderGameData.GetComponent<LeaderboardData>().setData(counter, null, 0);
        //    Level_3DataPanelList.Add(leaderGameData);
        //}

        //counter = 0;

        //yield return null;
    }


    public void ResetAllData()
    {
        //Delete them manually per level Data

        //Level1
        Level_1DataList.Clear();
        foreach (GameObject gameData in Level_1DataPanelList)
        {
            GameObject.Destroy(gameData);
        }
        Level_1DataPanelList.Clear();


        ////Level2
        //Level_2DataList.Clear();
        //foreach (GameObject gameData in Level_2DataPanelList)
        //{
        //    GameObject.Destroy(gameData);
        //}
        //Level_2DataPanelList.Clear();

        ////Level3


        //Level_3DataList.Clear();
        //foreach (GameObject gameData in Level_3DataPanelList)
        //{
        //    GameObject.Destroy(gameData);
        //}
        //Level_3DataPanelList.Clear();

    }


    //Dynamic Calling
    public void InsertList(GameObject PanelLocList, List<GameObject> Level_DataPanelList,  List<Dictionary<string, dynamic>> scoreList)
    {
        //Optional: create an if statement if have wifi

        //Determine if null the data or no items
        if(scoreList.Count == 0 || scoreList == null)
        {
            Debug.Log("Eempty Data || Check your internet Connection");
        }

        else
        {
            counter = 0;

            Debug.Log($"Count {scoreList.Count}");
            for (int i = 0; i < scoreList.Count; i++)
            //foreach (Dictionary<string, dynamic> leaderData in Level_1DataList)
            {
                //(Dictionary<string, dynamic> leaderData in Level_1DataList)
                Dictionary<string, dynamic> leaderData = Level_1DataList[i];


                //Data Parsing
                counter++;
                string Name = leaderData["user_name"];
                int score = (int)leaderData["score"];


                //Prefab Handling
                GameObject leaderGameData = GameObject.Instantiate(prefabData);
                leaderGameData.transform.SetParent(Level_1DataHolder.transform, false);

                leaderGameData.GetComponent<LeaderboardData>().setData(counter, Name, score);
                Level_DataPanelList.Add(leaderGameData);

                Debug.Log(counter);
            }

            //Contingency Plan if list has less than 10 items
            while (counter < 10)
            {
                counter++;
                GameObject leaderGameData = GameObject.Instantiate(prefabData);
                leaderGameData.transform.SetParent(Level_1DataHolder.transform, false);

                leaderGameData.GetComponent<LeaderboardData>().setData(counter, "Filler", 0);
                Level_DataPanelList.Add(leaderGameData);

                Debug.Log(counter);
            }
        }

    }



    //No Wifi Info
    private void NoInternetIndicator (GameObject PanelLocList){
        while (counter <= 10)
        {
            counter++;
            GameObject leaderGameData = GameObject.Instantiate(prefabData);
            leaderGameData.transform.SetParent(PanelLocList.transform, false);

            leaderGameData.GetComponent<LeaderboardData>().setData(0, "NO INTERNET TRY AGAIN LATER", 0);
            

            Debug.Log(counter);
        }
    }

}
