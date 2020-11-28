using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{
    public string PopUp;
    public PopUpSystem pop;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player1") == true)
        {
            
            Debug.Log("PopUp");
            this.gameObject.SetActive(false);
            
            pop.PopUp(PopUp);

        }
    }
}
