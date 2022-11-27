using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventsExtra : MonoBehaviour
{
    [SerializeField] GameObject confirmSection_1; //Confirm Tab: For Adding Currency

    private void Start()
    {
        //Set all Gameobject Reference to false
        confirmSection_1.SetActive(false);
    }

    public void OnEnableConfirmSection_1()
    {
        confirmSection_1.SetActive(true);
    }

    public void OnDisableConfirmSection_1()
    {
        confirmSection_1.SetActive(false);
    }
}
