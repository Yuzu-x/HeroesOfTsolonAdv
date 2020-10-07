using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string currentSceneName;

    public List<string> strategyScenes = new List<string>();

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;

        if(strategyScenes.Contains(currentSceneName))
        {

        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
