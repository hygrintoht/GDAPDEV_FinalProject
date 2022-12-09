using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField nameField;
    [SerializeField] Button playBtn;

    public void OnNameFieldValueChanged()
    {
        playBtn.interactable = nameField.text != "";
    }

    public void OnPressPlay()
    {
        Debug.Log("Play is pressed");
        PlayerPrefs.SetString("user_name", nameField.text);
        SceneManager.LoadScene(1);
    }

    public void OnPressQuit()
    {
        Debug.Log("Quit is pressed");
        Application.Quit();
    }
}
