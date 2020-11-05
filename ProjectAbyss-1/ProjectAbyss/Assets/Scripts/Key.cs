using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public LockedDoor porta; //porta associada a esta chave

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player1") == true)
        {
            porta.keyGot = true;
            Debug.Log("Pegada");
            this.gameObject.SetActive(false);
        }
    }

}
