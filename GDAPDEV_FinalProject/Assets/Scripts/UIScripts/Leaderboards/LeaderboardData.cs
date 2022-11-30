using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardData : MonoBehaviour
{
    [Header("Leaderboard Info")]
    [SerializeField] TextMeshProUGUI numTxt;
    [SerializeField] TextMeshProUGUI user_NameTxt;
    [SerializeField] TextMeshProUGUI scoreTxt;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setData(int num, string name, int score)
    {
      
        numTxt.text = num.ToString();

        if (name != null)
            user_NameTxt.text = name;

        if (score >= 0)
            scoreTxt.text = score.ToString();

    }
}
