using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    
    

   

    public void LoadMenu()
    {
        //volta ao menu principal e faz o tempo andar
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void RestartLevel()
    {
        //recomeça a cena atual e faz o tempo andar
        Debug.Log("Restarting");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        //fecha o jogo
        Debug.Log("Quit");
        Application.Quit();
    }
}
