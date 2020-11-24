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
    private Enemy thisGuy;


    public bool facingRight = true;
    private bool range = false;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player1").GetComponent<Transform>();
        thisGuy = this.gameObject.GetComponent<Enemy>();
        startPos = transform.position;
    }

    void Update()
    {

        if(range)
        {
            thisGuy.Attack();
        }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            range = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            range = false;
        }
    }
}
