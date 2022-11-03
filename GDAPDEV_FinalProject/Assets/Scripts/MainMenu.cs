using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OnPressPlay()
    {
        Debug.Log("Play is pressed");
        SceneManager.LoadScene(1);
    }

    public void OnPressQuit()
    {
        Debug.Log("Quit is pressed");
        Application.Quit();
    }
}
