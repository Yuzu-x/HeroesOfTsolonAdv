using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public GameObject menuObject;
    public bool menuActive = false;
    public GameObject pauseMenu;
    public bool pauseActive = false;
    public GameObject videoMenu;
    public bool videoActive = false;
    public GameObject audioMenu;
    public bool audioActive = false;
    public GameObject inputMenu;
    public bool inputActive = false;
    public GameObject saveLoadMenu;
    public bool saveLoadActive = false;

    public KeyCode pauseMenuKey = KeyCode.Escape;

    public GameObject partyManagementObject;
    public GameObject recruitManagementObject;
    public bool partyManagementActive = false;

    void Update()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Input.GetKeyDown(pauseMenuKey))
        {
            PauseMenu();
        }
    }

    public void PauseMenu()
    {
        if (!pauseActive)
        {
            menuObject.SetActive(true);
            menuActive = true;
            pauseMenu.SetActive(true);
            pauseActive = true;
        }
        else if (pauseActive)
        {
            menuObject.SetActive(false);
            menuActive = false;
            pauseMenu.SetActive(false);
            pauseActive = false;
        }
    }

    public void VideoMenu()
    {
        if(!videoActive)
        {
            pauseMenu.SetActive(false);
            pauseActive = false;
            videoMenu.SetActive(true);
            videoActive = true;
        }
        else if(videoActive)
        {
            videoMenu.SetActive(false);
            pauseMenu.SetActive(true);
            pauseActive = true;
            videoActive = false;
        }
    }

    public void AudioMenu()
    {
        if(!audioActive)
        {
            pauseMenu.SetActive(false);
            pauseActive = false;
            audioMenu.SetActive(true);
            audioActive = true;
        }
        else if(audioActive)
        {
            audioMenu.SetActive(false);
            pauseMenu.SetActive(true);
            pauseActive = true;
            audioActive = false;
        }
    }

    public void InputMenu()
    {
        if(!inputActive)
        {
            pauseMenu.SetActive(false);
            pauseActive = false;
            inputMenu.SetActive(true);
            inputActive = true;
        }
        else if(inputActive)
        {
            inputMenu.SetActive(false);
            pauseMenu.SetActive(true);
            pauseActive = true;
            inputActive = false;
        }
    }

    public void SaveLoadMenu()
    {
        if(!saveLoadActive)
        {
            pauseMenu.SetActive(false);
            pauseActive = false;
            saveLoadMenu.SetActive(true);
            saveLoadActive = true;
        }
        else if(saveLoadActive)
        {
            saveLoadMenu.SetActive(false);
            pauseMenu.SetActive(true);
            saveLoadActive = false;
            pauseActive = true;
        }
    }

    public void PartyButton()
    {
        if(!partyManagementActive && !pauseActive)
        {
            partyManagementObject.SetActive(true);
            recruitManagementObject.SetActive(true);
            partyManagementActive = true;
        }
        else if(partyManagementActive)
        {
            partyManagementObject.SetActive(false);
            recruitManagementObject.SetActive(false);
            partyManagementActive = false;
        }
    }
}
