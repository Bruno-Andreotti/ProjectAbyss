using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{
    public string PopUp;
    public PopUpSystem pop;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //quando colide com o player, desativa o objeto e liga a PopUp associada
        if (collision.collider.CompareTag("Player1") == true)
        {
            
            Debug.Log("PopUp");
            this.gameObject.SetActive(false);
            
            pop.PopUp(PopUp);

        }
    }
}
