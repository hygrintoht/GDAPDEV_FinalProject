using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameUIEvents : MonoBehaviour
{

    [Header("OptionPanel")]
    [SerializeField] private GameObject optionPanel;

    [Header("Tab Handling")]
    [SerializeField] private GameObject generalSection;
    [SerializeField] private GameObject cheatSection;

    private GameObject currActiveTab;
    

    // Start is called before the first frame update
    void Start()
    {
        currActiveTab = generalSection;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Public Calls
    public void onPressMainMenu()
    {
        SceneManager.LoadScene(1); //Load to loadout scene
        Time.timeScale = 1.0f;
    }

    public void onPressOption()
    {
        optionPanel.SetActive(true);
        Time.timeScale = 0.0f;

        currActiveTab = generalSection;
        currActiveTab.SetActive(true);
    }

    public void onPressBack()
    {
        currActiveTab.SetActive(false);

        optionPanel.SetActive(false);
        Time.timeScale = 1.0f;

    }

    public void onPressQuit()
    {
        Time.timeScale = 1.0f;
        Application.Quit();
    }

    //===== Settings Panel ======//
    public void selectConfigTab()
    {
        if(currActiveTab != generalSection)
        {
            currActiveTab.SetActive(false);
            currActiveTab = generalSection;
            generalSection.SetActive(true);
        }
        
    }

    public void selectDebugTab()
    {
        if (currActiveTab != cheatSection)
        {
            currActiveTab.SetActive(false);
            currActiveTab = cheatSection;
            cheatSection.SetActive(true);
        }
    }
    

    //private calls

}
