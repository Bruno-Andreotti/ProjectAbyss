using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseUI;
    public GameObject optionsUI;

    void Start()
    {
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //jogo é pausado e continuado apertando ESC.
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
        //desativa os menus, recomeça o tempo.
        pauseUI.SetActive(false);
        optionsUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        //ativa o menu de pausa, para o tempo
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //public void LoadMenu()
    //{
        //Time.timeScale = 1f;
      //  SceneManager.LoadScene("Menu");
    //}

    public void RestartLevel()
    {
        //recomeça a cena atual
        Debug.Log("Restarting");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        //volta para o menu principal
        Debug.Log("Quit");
        SceneManager.LoadScene("Menu2");
    }
}
