using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //chama a proxima cena depois dos 30 segundos de creditos
        Invoke("LoadMenu", 30f);
    }

    // Update is called once per frame
    void Update()
    {
        //dá a opçao de pular os creditos com Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMenu();
        }

    }
    void LoadMenu()
    {
        SceneManager.LoadScene("Menu2");
    }
}
