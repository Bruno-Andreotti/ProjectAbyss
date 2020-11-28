using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject LoaderUI;
   public void PlayGame()
    {
        LoaderUI.SetActive(true);
        Invoke("LoadNext", 6f);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    void LoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
