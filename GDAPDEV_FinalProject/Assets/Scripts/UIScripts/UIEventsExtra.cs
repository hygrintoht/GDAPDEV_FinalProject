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
        StartCoroutine(initData());

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


    /*Transfering Data Section*/
    IEnumerator initData()
    {

        //yield return StartCoroutine(loadAllLeaderboard());
        //Load All Data
        Level_1DataList = referenceWebAPI.returnLevel1_SetScores();
        Level_2DataList = referenceWebAPI.returnLevel2_SetScores();
        Level_3DataList = referenceWebAPI.returnLevel3_SetScores();

        //Create a prefab for everysingle one

        int counter = 0;


        //Level 1
        for (int i = 0; i < Level_1DataList.Count; i++)
        {
            Dictionary<string, dynamic> leaderData = Level_1DataList[i];
            //if (leaderData != null)
            //{
            //    Debug.Log("Not Empty");
            //}

            //Data Parsing
            counter++;
            string Name = leaderData["user_name"] as string;
            int score = leaderData["score"];

            
            //Prefab Handling
            GameObject leaderGameData = GameObject.Instantiate(prefabData);
            leaderGameData.transform.SetParent(Level_1DataHolder.transform, false);

            leaderGameData.GetComponent<LeaderboardData>().setData(counter, null, score);
            Level_1DataPanelList.Add(leaderGameData);

            Debug.Log(i);

        }

        while (counter < 10)
        {
            counter++;
            GameObject leaderGameData = GameObject.Instantiate(prefabData);
            leaderGameData.transform.SetParent(Level_1DataHolder.transform, false);

            leaderGameData.GetComponent<LeaderboardData>().setData(counter, null, 0);
            Level_1DataPanelList.Add(leaderGameData);
        }

        counter = 0;

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

        yield return null;
    }


    IEnumerator loadAllLeaderboard()
    {
        //Load All Data
        Level_1DataList = referenceWebAPI.returnLevel1_SetScores();
        if(Level_1DataList == null)
        {
            Debug.Log("Empty");
        }


        //Level_2DataList = referenceWebAPI.returnLevel2_SetScores();
        //Level_3DataList = referenceWebAPI.returnLevel3_SetScores();

        //Rest Time
        yield return new WaitForSeconds(5.0f);

    }


}
