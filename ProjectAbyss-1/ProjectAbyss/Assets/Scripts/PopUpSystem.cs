using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    
    //public Image image;
    

    public void PopUp(string text)
    {
        //ativa a UI de PopUps e prepara pra pausar o tempo
        popUpBox.SetActive(true);
        popUpText.text = text;
        //i.sprite = image.sprite;
        animator.SetTrigger("pop");
        Invoke("Pause", 0.5f);
        
    }
    void Pause()
    {
        //para o tempo
        Time.timeScale = 0f;
        //GameIsPaused = true;
    }
    public void Resume()
    {
        //despausa o tempo apos clicar no botao
        Time.timeScale = 1f;
    }

}
