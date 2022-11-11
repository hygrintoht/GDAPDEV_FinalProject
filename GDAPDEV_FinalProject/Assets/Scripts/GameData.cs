using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData: MonoBehaviour
{
    //Singleton Design Pattern
    public static GameData Instance { get; private set; }

    //Max Potential bar per each upgrades
    private int maxHealthTick = 4;
    private int maxShieldTick = 4;
    private int maxReviveTick = 1;
    private int maxDamageTick = 8;
    private int maxAttkSpdTick = 8;

    //Player Data
    //Create List to temporary store the data as tick
    private int currHealthTick = 0;
    private int currShieldTick = 0;
    private int currReviveTick = 0;
    private int currDamageTick = 0;
    private int currAttkSpdTick = 0;
   

    //Other useful Data
    // private int score = 0;
    // private int highScore = 0;
    private int totalCurrency = 2000;

    //Cheat Data
    //bool isUnliHealth
    //bool isOneHitKill


    private void Awake()
    {

        if (Instance != null)
            Destroy(this);


        Instance = this;
        LoadAll();
    }
//public function
   public List<int> retrieveMaxData()
    {
        //Debug.Log("Pass Data");
        List<int> maxData = new List<int>();

        //Insert all data
        maxData.Add(maxHealthTick);
        maxData.Add(maxShieldTick);
        maxData.Add(maxReviveTick);
        maxData.Add(maxDamageTick);
        maxData.Add(maxAttkSpdTick);
        

        Debug.Log($"Count: {maxData[0]}");

        return maxData;
    }

    public List<int> retrieveCurrentData()
    {
        List<int> currData = new List<int>();
        
        currData.Add(currHealthTick);
        currData.Add(currShieldTick);
        currData.Add(currReviveTick);
        currData.Add(currDamageTick);
        currData.Add(currAttkSpdTick);
       

        return currData;
    }


    public void IncrementData(int type)
    {
        switch (type)
        {
            case 1: //healthTick
                if (currHealthTick < maxHealthTick)
                    currHealthTick++;
                break;
            case 2: //shieldTick
                if (currShieldTick < maxShieldTick)
                    currShieldTick++;
                break;
            case 3: //revsTick
                if (currReviveTick < maxReviveTick)
                    currReviveTick++;
                break;
            case 4: //damageTick
                if (currDamageTick < maxDamageTick)
                    currDamageTick++;
                break;
            case 5: //attckSpeedTick
                if (currAttkSpdTick < maxAttkSpdTick)
                    currAttkSpdTick++;
                break;
            
        }

        StoreAll();

    }

    public void printData()
    {
        //Debug.Log("Data Found");
    }

    //public void retrieveData;

    public int RetrieveCurrency()
    {
        return totalCurrency;
    }

    public void UpdateCurrency(int increments)
    {
        totalCurrency += increments;
        StoreAll();
    }

    public void ResetData()
    {
        //Currency
        totalCurrency = 0;


        //Upgrade Stats
        currHealthTick = 0;
        currShieldTick = 0;
        currReviveTick = 0;
        currDamageTick = 0;
        currAttkSpdTick = 0;

        StoreAll();
    }

    public void StoreAll()
    {
        Debug.Log("Stored Datal");

        PlayerPrefs.SetInt("currHealthTick", currHealthTick);
        PlayerPrefs.SetInt("currShieldTick", currShieldTick);
        PlayerPrefs.SetInt("currReviveTick", currReviveTick);
        PlayerPrefs.SetInt("currDamageTick", currDamageTick);
        PlayerPrefs.SetInt("currAttkSpdTick", currAttkSpdTick);

        //Currency Data
        PlayerPrefs.SetInt("totalCurrency", totalCurrency);
    }

    //private function



    private void LoadAll()
    {
        Debug.Log("Load All");
        //Upgrade Data
        currHealthTick = PlayerPrefs.GetInt("currHealthTick");
        currShieldTick = PlayerPrefs.GetInt("currShieldTick");
        currReviveTick = PlayerPrefs.GetInt("currReviveTick");
        currDamageTick = PlayerPrefs.GetInt("currDamageTick");
        currAttkSpdTick = PlayerPrefs.GetInt("currAttkSpdTick");


        //Currency Data
        totalCurrency = PlayerPrefs.GetInt("totalCurrency");

        //Cheat Data
        
    }

    
    

}
