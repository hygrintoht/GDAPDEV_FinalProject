using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    //Singleton Design Pattern
    public static GameData Instance { get; private set; }

    //Max Potential bar per each upgrades
    private int maxHealthTick = 4;
    private int maxDamageTick = 8;
    private int maxAttkSpdTick = 8;
    private int maxShieldTick = 4;
    private int maxReviveTick = 1;

    //Player Data
    //Create List to temporary store the data as tick
    private int currHealthTick = 0;
    private int currDamageTick = 0;
    private int currAttkSpdTick = 0;
    private int currShieldTick = 0;
    private int currReviveTick = 0;

    //Other useful Data
    // private int score = 0;
    //private int highScore = 0;
    //private int totalCurrency = 0;


    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
    }
//public function
   public List<int> retrieveMaxData()
    {
        List<int> maxData = null;

        //Insert all data
        maxData.Add(maxHealthTick);
        maxData.Add(maxDamageTick);
        maxData.Add(maxAttkSpdTick);
        maxData.Add(maxShieldTick);
        maxData.Add(maxReviveTick);

        return maxData;
    }

    public List<int> retrieveCurrentData()
    {
        List<int> currData = null;

        currData.Add(currHealthTick);
        currData.Add(currDamageTick);
        currData.Add(currAttkSpdTick);
        currData.Add(currShieldTick);
        currData.Add(currReviveTick);

        return currData;
    }


    public void incrementData(int type)
    {
        switch (type)
        {
            case 1: //healthTick
                if (currHealthTick < maxHealthTick)
                    currHealthTick++;
                break;
            case 2: //damageTick
                if (currDamageTick < maxDamageTick)
                    currDamageTick++;
                break;
            case 3: //attckSpeedTick
                if (currAttkSpdTick < maxAttkSpdTick)
                    currAttkSpdTick++;
                break;
            case 4: //shieldTick
                if (currShieldTick < maxShieldTick)
                    currShieldTick++;
                break;
            case 5: //revsTick
                if (currReviveTick < maxReviveTick)
                    currReviveTick++;
                break;
        }

    }


    //public void retrieveData;

    //private function
    

}
