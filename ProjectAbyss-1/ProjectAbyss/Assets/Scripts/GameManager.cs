using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public GameObject WinGameUI;

    //public float restartDelay= 0f;

        public void LevelWin()
    {
        Debug.Log("Vitória");
        WinGameUI.SetActive(true);
    }
    // Start is called before the first frame update
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Derrota");
            Restart();
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
