using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public GameObject LoseGameUI;
    public CharacterController2D player;
    //public float restartDelay= 0f;

        
    
    public void EndGame()
    {
        player.enabled = false;

        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Derrota");
            Time.timeScale = 0f;
            LoseGameUI.SetActive(true);
        }

    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
