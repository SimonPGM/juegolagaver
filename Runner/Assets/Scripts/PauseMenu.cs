using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private String mainMenu;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }    
    }

    public void Resume()
    {
        Paused = false;
        pauseMenuUI.SetActive(Paused);
        Time.timeScale = 1f;
    }
    void Pause()
    {
        Paused = true;
        pauseMenuUI.SetActive(Paused);
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Resume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
