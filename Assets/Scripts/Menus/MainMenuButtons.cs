using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public Scene currentScene;

    public string loadGameScene;

    public string newGameSceneName = "WorldScene";
    public GameObject settingsMenuCanvas;
    public GameObject loadGameMenuCanvas;

    public void LoadGameButton()
    {
        loadGameMenuCanvas.SetActive(true);
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene(newGameSceneName);
    }

    public void GameSettingsButton()
    {
        settingsMenuCanvas.SetActive(true);
    }
}
