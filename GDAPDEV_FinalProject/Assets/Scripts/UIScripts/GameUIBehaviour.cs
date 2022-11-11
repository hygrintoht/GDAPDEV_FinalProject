using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIBehaviour : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject healthBarObj;
    [SerializeField] Image healthFillBar;
    [SerializeField] GameObject progBarObj;
    [SerializeField] Image progFillBar;

    [Header("GameData")]
    [SerializeField] private ShipControls shipInfo;
    [SerializeField] private float timer = 10.0f;

    private float currentTime = 0;
    
    //EnemyBehavior

    //For Health
    Color origHealthColor;
    Color dangerColor;

    //For Prog
    Color origProgColor;
    Color bossColor;

    private Slider healthBar;
    private Slider progBar;



    // Start is called before the first frame update
    void Start()
    {
        //Color Init
        InitColor();
        //Get component for the slider   
        InitBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBar();
        currentTime = currentTime + Time.deltaTime;

    }

    public void InitColor()
    {
        //Health
        origHealthColor = healthFillBar.color;
        dangerColor = new Color(1.0f, 0.5f, 0);

        //Prog
        origProgColor = progFillBar.color;
        bossColor = Color.red;
    }

    public void InitBar()
    {
        healthBar = healthBarObj.GetComponent<Slider>();
        progBar = progBarObj.GetComponent<Slider>();

        
    }

    //public function
    public void UpdateBar()
    {
        //For Health
        int playerHealth = shipInfo.HP;
        int maxHP = shipInfo.maxHP;

        float healthRatio = (float) playerHealth / (float) maxHP;

        if(healthRatio <= 0.4f)
        {
           healthFillBar.color = dangerColor;
        }

        
        healthBar.value = healthRatio;

        //For Progress
        float progressRatio = currentTime / timer;
        

        progBar.value = progressRatio;
    }

    

    

}
