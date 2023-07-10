using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public float tempo = 1f;
    public static bool GameIsPaused = false;
    

    public GameObject pauseMenuUI;
    public GameObject UiDoJogoUI;
    

    void Update()
    {
        Time.timeScale = tempo;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
        pauseMenuUI.SetActive(false);
        UiDoJogoUI.SetActive(true);

        tempo = 1f;
        GameIsPaused = false;
       
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        UiDoJogoUI.SetActive(false);
        tempo = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()

    {
        tempo = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }


}