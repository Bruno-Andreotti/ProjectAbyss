using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public bool keyGot; //associar qual chave é pra qual porta diretamente aqui
    public float portaRotate;
    public Transform porta;
    public Collider2D colisor;
    // Start is called before the first frame update
    void Start()
    {
        keyGot = false;
    }

    private void Open()
    {
        porta.Rotate(0.0f, portaRotate, 0.0f);
        colisor.enabled = false;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (keyGot)
        {
            Debug.Log("dooropen");
            Open();
        }
    }     
}
