using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UpgradeEvent : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] Button YesBtn;
    [SerializeField] TextMeshProUGUI UpgradeConfirmPromptTxt;


    private int upgradeSet;
    private int upgradeCost;

    //Default info
    private string defaultAns;


    // Start is called before the first frame update
    void Start()
    {
        defaultAns = YesBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    private void OnEnable()
    {
        UpgradeConfirmPromptTxt.text = $"Do you wish to pay {upgradeCost} \n for this upgrade ? ";
        OnCheckStatus();

    }

    private void OnDisable()
    {
        //Update the properties
        if (defaultAns != null)
            YesBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = defaultAns;

        else
            Debug.LogError("Yes Comment is missing");

        YesBtn.interactable = true;
    }

    public void UpdateUpgradeData(int type, int cost)
    {
        upgradeSet = type;
        upgradeCost = cost;
    }

    private void OnCheckStatus()
    {
        int currMoney = GameData.Instance.RetrieveCurrency();

        if (currMoney > upgradeCost)
        {
            YesBtn.interactable = true;
        }

        else
        {
            TextMeshProUGUI infoMessage = YesBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            infoMessage.text = "Not enough Money";
            YesBtn.interactable = false;
        }
    }


    public void OnConfirm()
    {
        GameData.Instance.IncrementData(upgradeSet);
        GameData.Instance.UpdateCurrency(-(upgradeCost));
        gameObject.SetActive(false);
    }

    public void OnDisagree()
    {
        gameObject.SetActive(false);
    }

    private void UpdateData()
    {

    } 

        

}
