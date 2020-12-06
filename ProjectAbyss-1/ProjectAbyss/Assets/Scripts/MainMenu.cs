using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject LoaderUI;
   public void PlayGame()
    {
        //carrega a primeira cena depois de mostrar a tela de carregamento
        LoaderUI.SetActive(true);
        Invoke("LoadNext", 6f);
    }
    public void QuitGame()
    {
        //fecha o jogo
        Debug.Log("Quit");
        Application.Quit();
    }
    void LoadNext()
    {
        //carrega a primeira cena(Intro)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadCredits()
    {
        //carrega a cena de créditos
        SceneManager.LoadScene("Creditos");
    }

}
