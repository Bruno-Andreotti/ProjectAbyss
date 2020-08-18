using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    
    
    public GameObject Gate;
    public GameObject Gate2;

    void Start()
    {
        
        //GateArr = new GameObject[gateNum];
       // for (int i = 0; i < gateNum; i++)
        //{
            //GameObject go = Instantiate(Gate, new Vector3((float)i, 1, 0), Quaternion.identity) as GameObject;
            //go.transform.localScale = Vector3.one;
           // GateArr[i] = go;
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Gate.transform.position =Gate.transform.position + Gate.transform.up * 600;
        Gate2.transform.position = Gate2.transform.position + Gate2.transform.up * 600;
        Debug.Log("entrou na colisão");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Gate.transform.position = Gate.transform.position + Gate.transform.up * -600;
        Gate2.transform.position = Gate2.transform.position + Gate2.transform.up * -600;
        Debug.Log("saiu da colisão");
    }
}
