using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    public GameObject LoaderUI;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //em colisão com o player, ativa a tela de loading e prepara para carregar a proxima cena
        if (collision.gameObject.name == "Player (1)")
        {
            //FindObjectOfType<GameManager>().LevelWin();
            LoaderUI.SetActive(true);
            Invoke("LoadNext", 6f);
        }
    }

    void LoadNext()
    {//carrega a proxima cena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}

