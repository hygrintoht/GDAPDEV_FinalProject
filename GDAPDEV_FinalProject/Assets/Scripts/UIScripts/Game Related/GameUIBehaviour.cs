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
    [SerializeField] private float timer = 10.0f;
    [SerializeField] private float score_Multiplier = 1.0f;

    [SerializeField] GameObject boss;

    private float currentTime = 0;
    private bool hasDataUploaded = false;
    private bool bossActive = true;
    
    //EnemyBehavior

    //For Health
    Color origHealthColor;
    Color dangerColor;

    //For Prog
    Color origProgColor;
    Color bossColor;

    private Slider healthBar;
    private Slider progBar;


    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_HIT, this.UpdateHealth);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_HIT);
    }

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

        //For Progress
        float progressRatio = currentTime / timer;

        if (progressRatio > .95f)
        {
            progFillBar.color = bossColor;
            bossLabel.SetActive(true);
            SpawnerGroup.GetInstance().ChangeSpwanersActiveStatus(false);


            if (bossActive == true)
            {
                if (progressRatio > 1.0f)
                {
                    boss.SetActive(true);
                }

                if ((timer + 5.0f) < currentTime)
                {
                    progBarObj.SetActive(false);
                    //play audio if needed

                    if (boss.GetComponent<BossBehavior>().health <= 0)
                    {
                        scoreSection.SetActive(true);
                        Time.timeScale = 0;



                        if (!hasDataUploaded)
                        {
                            GameData.Instance.UpdateScore(2000);
                            int scores = GameData.Instance.RetrieveScore();

                            //Text Description
                            scoreTxt.text = $"Total Score: {scores.ToString()} ";
                            multiplierTxt.text = $"Score Multiplier: {score_Multiplier.ToString()} X";
                            totalEarningTxt.text = $"Currency Earned: {scores * score_Multiplier} ";

                            float earnings = scores * score_Multiplier;
                            GameData.Instance.UpdateCurrency((int)earnings);
                            hasDataUploaded = true;
                            bossActive = false;
                        }
                    }
                }
            }
        }
        progBar.value = progressRatio;
    }

    private void UpdateHealth(Parameters param)
    {
        
        //For Health
        int playerHP = param.GetIntExtra("playerHealth", 0);
        int maxHP = param.GetIntExtra("maxHealth", 0);

        float healthRatio = (float)playerHP / (float)maxHP;

        if (healthRatio <= 0.4f)
        {
            healthFillBar.color = dangerColor;
        }

        healthBar.value = healthRatio;

        if (playerHP <= 0)
        {
            ScoreUpdate();
        }
    }

    public void ScoreUpdate()
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
