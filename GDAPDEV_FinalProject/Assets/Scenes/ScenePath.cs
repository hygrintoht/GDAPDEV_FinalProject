using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePath : MonoBehaviour
{
    public void NameFromIndex(int buildIndex)
    {
        Debug.Log(SceneUtility.GetScenePathByBuildIndex(buildIndex));
        //SceneManager.LoadScene(SceneUtility.GetScenePathByBuildIndex(buildIndex));
    }
    
}
