using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    public float speed;
    public float distance;
    
    public GameObject childRenderer;

    private bool movingRight = true;
    private Enemy thisGuy;
    private Transform player;
    public Transform limitDetection;

    private void Start()
    {
        thisGuy = this.gameObject.GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player1").transform;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(limitDetection.position, Vector2.down, distance);


        if (Vector2.Distance(transform.position, player.position) < distance)
        {
            RaycastHit2D sightInfo = Physics2D.Raycast(thisGuy.shootPoint.position, thisGuy.shootPoint.right);
            if (sightInfo.collider.CompareTag("Player1") == true)
            {
                //Aqui seria o ponto ideal para iniciar o estado Chase/Atacando
                speed = 0;
                thisGuy.Attack();

            }
            else speed = 2; //usar uma variável no futuro
        }


        if (groundInfo.collider.CompareTag("Limiter") == true)
        {
            Debug.Log("Limite");
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                childRenderer.transform.Rotate(0f, 180f, 0f);
                childRenderer.GetComponent<SpriteRenderer>().flipX = !childRenderer.GetComponent<SpriteRenderer>().flipX;
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                childRenderer.transform.Rotate(0f, 180f, 0f);
                childRenderer.GetComponent<SpriteRenderer>().flipX = !childRenderer.GetComponent<SpriteRenderer>().flipX;
                movingRight = true;
            }
        }     
    }
}
