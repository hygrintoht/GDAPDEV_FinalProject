using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

public class WebAPI : MonoBehaviour
{
    public readonly string BaseURL = "https://gdapdev-web-api.herokuapp.com/api/";
    //[SerializeField] private string Pass = "";


    private List<Dictionary<string, dynamic>> Level_1DataList;
    private List<Dictionary<string, dynamic>> Level_2DataList;
    private List<Dictionary<string, dynamic>> Level_3DataList;


    private void Start()
    {
        Level_1DataList = new List<Dictionary<string, dynamic>>();
        Level_2DataList = new List<Dictionary<string, dynamic>>();
        Level_3DataList = new List<Dictionary<string, dynamic>>();

        LoadDataAll();
    }

    //Data Loading

    public void LoadDataAll()
    {
        StartCoroutine(LoadAll());
    }

    IEnumerator LoadAll()
    {
        yield return StartCoroutine(SamplePlayersScoreRequest(101));
        //yield return StartCoroutine(SamplePlayersScoreRequest(102));
        //yield return StartCoroutine(SamplePlayersScoreRequest(103));

    }


    public List<Dictionary<string, dynamic>> ReturnLevel1_SetScores()
    {
        return Level_1DataList;
    }

    public List<Dictionary<string, dynamic>> ReturnLevel2_SetScores()
    {
        return Level_2DataList;
    }

    public List<Dictionary<string, dynamic>> ReturnLevel3_SetScores()
    {
        return Level_3DataList;
    }


    //Debug for single push
    public void Debug_SendPlayerData_L01()
    {
        StartCoroutine(SendScorePostRequest(101, "DebugPlayer", 0));
    }

    public void Debug_SendPlayerData_L02()
    {
        StartCoroutine(SendScorePostRequest(102, "DebugPlayer", 0));
    }

    public void Debug_SendPlayerData_L03()
    {
        StartCoroutine(SendScorePostRequest(103, "DebugPlayer", 0));
    }


    //Reset Debug
    public void ResetAllScoreAPI()
    {
        StartCoroutine(ResetPlayerScoreRequest(101, "01"));
        StartCoroutine(ResetPlayerScoreRequest(102, "02"));
        StartCoroutine(ResetPlayerScoreRequest(103, "03"));
    }


    //================== Web API ======================//



    public void SendScore(int number, string Name, int score) //add arguments later (number, Name, score)
    {
        StartCoroutine(SendScorePostRequest(number, Name, score));
    }

    IEnumerator SendScorePostRequest(int number, string Name, int score)
    {




        //Dictionary to contain the parameters to create a player;
        Dictionary<string, dynamic> PlayerParams = new Dictionary<string, dynamic>();

        PlayerParams.Add("group_num", number);
        PlayerParams.Add("user_name", Name);
        PlayerParams.Add("score", score);
       

        //Turns dictionary into a JSON String

        string requestString = JsonConvert.SerializeObject(PlayerParams);

        //Convert the string into bytes
        byte[] requestData = Encoding.UTF8.GetBytes(requestString);


        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "scores", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            request.uploadHandler = new UploadHandlerRaw(requestData);

            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            Debug.Log($"Response Code: {request.responseCode}");

            //Check if have errors;
            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log($"Message: {request.downloadHandler.text}");
            }

            else
            {
                Debug.LogError($"Error: {request.error}");
            }


        }

    }

    public void GetAllPlayerScores(int groupID)
    {
        StartCoroutine(SamplePlayersScoreRequest(groupID));
    }

    IEnumerator SamplePlayersScoreRequest(int groupID)
    {
        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "groups/" + groupID.ToString(), "GET"))
        {

            request.downloadHandler = new DownloadHandlerBuffer();

            Debug.Log("Sending got request.....");
            yield return request.SendWebRequest();

            Debug.Log($"Get all players response code: {request.responseCode}");

            //Check if have errors;
            if (string.IsNullOrEmpty(request.error))
            {
                //Debug.Log($"Message: {request.downloadHandler.text}");
                List<Dictionary<string, dynamic>> playerList = JsonConvert.
                   DeserializeObject<List<Dictionary<string, dynamic>>>(request.downloadHandler.text);

                //Handling different cases
                switch (groupID)
                {
                    case 101:
                        Level_1DataList = playerList;
                        break;
                    case 102:
                        Level_2DataList = playerList;
                        break;
                    case 103:
                        Level_3DataList = playerList;
                        break;

                    
                }

                foreach (Dictionary<string, dynamic> player in playerList)
                {
                    Debug.Log($"Got Player: {player["user_name"]}");
                    //player
                }


            }   

            else
            {
                Debug.LogError($"Error: {request.error}");
            }


        }


    }

    /*==========Debug===============*/

    public void ResetAllPlayerScores(int groupID, string secretPass)
    {
        StartCoroutine(ResetPlayerScoreRequest(groupID, secretPass));
    }

    IEnumerator ResetPlayerScoreRequest(int groupID, string secretPass)
    {
        //Dictionary to contain the parameters to create a player;
        Dictionary<string, dynamic> PlayerParams = new Dictionary<string, dynamic>();

        PlayerParams.Add("group_num", groupID);
        PlayerParams.Add("secret", $"L_{secretPass}");

        //Turns dictionary into a JSON String

        string requestString = JsonConvert.SerializeObject(PlayerParams);

        //Convert the string into bytes
        byte[] requestData = Encoding.UTF8.GetBytes(requestString);


        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "scores", "DELETE"))

        {
            request.SetRequestHeader("Content-Type", "application/json");

            request.uploadHandler = new UploadHandlerRaw(requestData);

            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            Debug.Log($"Response Code: {request.responseCode}");

            //Check if have errors;
            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log($"Message: {request.downloadHandler.text}");
            }

            else
            {
                Debug.LogError($"Error: {request.error}");
            }

        }
    }



    /*=======External Usage=============*/
    public void CreateGroup()
    {
        StartCoroutine(CreateGroupPostRequest());
    }

    IEnumerator CreateGroupPostRequest()
    {

        //Dictionary to contain the parameters to create a player;
        Dictionary<string, dynamic> PlayerParams = new Dictionary<string, dynamic>();

        PlayerParams.Add("group_num", 60);
        PlayerParams.Add("group_name", "Test: Group");
        PlayerParams.Add("game_name", "Test: Game Name");
        PlayerParams.Add("secret", "test01");

        //Turns dictionary into a JSON String

        string requestString = JsonConvert.SerializeObject(PlayerParams);

        //Convert the string into bytes
        byte[] requestData = Encoding.UTF8.GetBytes(requestString);


        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "groups", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            request.uploadHandler = new UploadHandlerRaw(requestData);

            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            Debug.Log($"Response Code: {request.responseCode}");

            //Check if have errors;
            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log($"Message: {request.downloadHandler.text}");
            }

            else
            {
                Debug.LogError($"Error: {request.error}");
            }


        }

    }

}
