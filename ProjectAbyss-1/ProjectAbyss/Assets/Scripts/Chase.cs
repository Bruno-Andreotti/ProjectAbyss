using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Chase : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;//alcance de perseguição

    public Animator animator;
    private Transform target;
    private Vector2 startPos;
    public GameObject childRenderer;
    private Enemy thisGuy;


    public bool facingRight = true;
    private bool range = false;//diz se o jogador está dentro do alcance de ataque(colisão)


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player1").GetComponent<Transform>();
        thisGuy = this.gameObject.GetComponent<Enemy>();
        startPos = transform.position;
    }

    void Update()
    {

        if(range)//invoca o metodo Attack da classe Enemy quando dentro do alcance de dano
        {
            thisGuy.Attack();
        }

        animator.SetBool("isMoving", false);
        if (Vector2.Distance(transform.position, target.position)< stoppingDistance)
        {//faz com que o inimigo corra na direção do jogador quando dentro do alcance de perseguição
            
            transform.position = Vector2.MoveTowards
                (transform.position, target.position, speed * Time.deltaTime);
            animator.SetBool("isMoving", true);

            if (target.transform.position.x < gameObject.transform.position.x && facingRight)
                Flip();
            if (target.transform.position.x > gameObject.transform.position.x && !facingRight)
                Flip();

        }
        else if (Vector2.Distance(transform.position, target.position) < stoppingDistance)
        
        {//faz com que o inimigo pare quando o jogador sai do alcance de perseguição
            
            transform.position = startPos.normalized;
            
        }
    }
    void Flip()
    {
        //um metodo para flipar o sprite do renderizador sem que ocorra problemas com os filhos do objeto ou com o shader
        
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        childRenderer.transform.Rotate(0f, 180f, 0f);
        childRenderer.GetComponent<SpriteRenderer>().flipX = !childRenderer.GetComponent<SpriteRenderer>().flipX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//dentro do alcance de ataque
        if (collision.gameObject.CompareTag("Player1"))
        {
            range = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {//fora do alcance de ataque
        if (collision.gameObject.CompareTag("Player1"))
        {
            range = false;
        }
    }
}
