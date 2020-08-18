using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Chase : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;

    private Transform target;
    private Vector2 startPos;

    public bool facingRight = false;



    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        startPos = transform.position;
    }

    void Update()
    {
       

        if (Vector2.Distance(transform.position, target.position)< stoppingDistance)
        {
            transform.position = Vector2.MoveTowards
                (transform.position, target.position, speed * Time.deltaTime);

            if (target.transform.position.x < gameObject.transform.position.x && facingRight)
                Flip();
            if (target.transform.position.x > gameObject.transform.position.x && !facingRight)
                Flip();
        }
        else if (Vector2.Distance(transform.position, target.position) < stoppingDistance)
        {
            transform.position = startPos.normalized;
        }
    }
    void Flip()
    {
        
        facingRight = !facingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1;
        gameObject.transform.localScale = tmpScale;
    }
    /* private void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.tag == "Player")
         {

             Debug.Log("Derrota");
         }
     }*/
}
