using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Chama o metodo de carregar a proxima cena, após os 36 segundos de introdução
        Invoke("LoadNext", 36f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
