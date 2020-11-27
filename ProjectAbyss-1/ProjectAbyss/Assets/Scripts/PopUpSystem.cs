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
        popUpBox.SetActive(true);
        popUpText.text = text;
        //i.sprite = image.sprite;
        animator.SetTrigger("pop");
    }

}
