using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameUIBehaviour : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject healthBarObj;
    [SerializeField] Image healthFillBar;
    [SerializeField] GameObject progBarObj;
    [SerializeField] Image progFillBar;
    [SerializeField] GameObject bossLabel;

    [Header("UI Score Place Holder")]
    [SerializeField] GameObject scoreSection;
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI multiplierTxt;
    [SerializeField] TextMeshProUGUI totalEarningTxt;

    [Header("GameData")]
    [SerializeField] private ShipControls shipInfo;
    [SerializeField] private float timer = 10.0f;
    [SerializeField] private float score_Multiplier = 1.0f;
    


    private float currentTime = 0;
    private bool hasDataUploaded = false;
    
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

        ScoreUpdate();

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
        
        if(progressRatio > .95f)
        {
            progFillBar.color = bossColor;
            bossLabel.SetActive(true);
            SpawnerGroup.GetInstance().ChangeSpwanersActiveStatus(false);
            if (progressRatio > 1.0f)
            {
                BossBehavior.GetInstance().gameObject.SetActive(true);
            }

            if((timer + 5.0f) < currentTime)
            {
                progBarObj.SetActive(false);
                //play audio if needed
            }
        }
        progBar.value = progressRatio;
    }

    public void ScoreUpdate()
    {
        if (shipInfo.HP <= 0)
        {
            scoreSection.SetActive(true);
            Time.timeScale = 0;

            int scores = GameData.Instance.RetrieveScore();

            //Text Description
            scoreTxt.text = $"Total Score: {scores.ToString()} ";
            multiplierTxt.text = $"Score Multiplier: {score_Multiplier.ToString()} X";
            totalEarningTxt.text = $"Currency Earned: {scores * score_Multiplier} ";

            if (!hasDataUploaded)
            {
                float earnings = scores * score_Multiplier;
                GameData.Instance.UpdateCurrency((int)earnings);
                hasDataUploaded = true;
            }

        }


    }

    

}
