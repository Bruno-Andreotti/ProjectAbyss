using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Chase : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;

    public Animator animator;
    private Transform target;
    private Vector2 startPos;
    public GameObject childRenderer;
    


    public bool facingRight = true;



    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player1").GetComponent<Transform>();

        startPos = transform.position;
    }

    void Update()
    {

        animator.SetBool("isMoving", false);
        if (Vector2.Distance(transform.position, target.position)< stoppingDistance)
        {
            
            transform.position = Vector2.MoveTowards
                (transform.position, target.position, speed * Time.deltaTime);
            animator.SetBool("isMoving", true);

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
        transform.Rotate(0f, 180f, 0f);
        childRenderer.transform.Rotate(0f, 180f, 0f);
        childRenderer.GetComponent<SpriteRenderer>().flipX = !childRenderer.GetComponent<SpriteRenderer>().flipX;
    }
    /* private void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.tag == "Player")
         {

             Debug.Log("Derrota");
         }
     }*/
}
