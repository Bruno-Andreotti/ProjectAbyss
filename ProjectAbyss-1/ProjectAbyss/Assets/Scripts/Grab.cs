using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    float throwForce = 1000;
    Vector2 objectPos;
    float distance;

    public bool canhold = true;
    public GameObject tempParent;
    GameObject item ;
    public bool isHolding = false;
    // Start is called before the first frame update
    void Start()
    {
        //item = GameObject.FindGameObjectWithTag("Grabbable");   
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(item.transform.position, tempParent.transform.position);
        if(distance>= 1f)
        {
            isHolding = false;
        }

        if(isHolding == true)
        {
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            item.GetComponent<Rigidbody2D>().angularVelocity = 0;
            item.transform.SetParent(tempParent.transform);
            item.transform.position = tempParent.transform.position;
            //item.GetComponent<Rigidbody2D>().isKinematic = true;
            item.GetComponent<Collider2D>().enabled = false;

            /*if(Input.GetKeyDown(KeyCode.R))
            {
                item.GetComponent<Rigidbody2D>().AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
            }*/


        }
        else
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.transform.position = objectPos;
            //item.GetComponent<Rigidbody2D>().isKinematic = false;
            item.GetComponent<Collider2D>().enabled = true;
        }
        if (isHolding == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isHolding = true;


            }
        }
        if (isHolding == true)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                item.GetComponent<Rigidbody2D>().AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHolding == false)
        {
            if (collision.gameObject.CompareTag("Grabbable"))
            {
                item = collision.gameObject;

            }
        }
    }
}
