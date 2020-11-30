using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePopUp : MonoBehaviour
{
    public GameObject extraDialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collisioninfo)
    {
        if (collisioninfo.collider.tag == "Player1")
        {

            Invoke("Delay", 0.5f);


        }

    }
    void Delay()
    {
        extraDialogue.SetActive(true);
    }
}
