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
        //desliga o jogador, mostra a UI de derrota, e para o tempo
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
        //recarrega a cena atual e reativa o jogador
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.enabled = true;
    }
}
