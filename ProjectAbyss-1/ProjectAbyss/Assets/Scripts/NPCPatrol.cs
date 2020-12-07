﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPatrol : MonoBehaviour
{ 
 public float speed;
public float distance;

public GameObject childRenderer;

private bool movingRight = true;


public Transform limitDetection;

private void Start()
{
    
}

private void Update()
{
        //os NPCs se movimentam desde o inicio, até os limitadores no ambiente, e então viram e andam para o outro lado. É uma versão simplificada do codigo de patrulha dos inimigos,
        //que nao leva em conta a existencia do jogador
    transform.Translate(Vector2.right * speed * Time.deltaTime);

    RaycastHit2D groundInfo = Physics2D.Raycast(limitDetection.position, Vector2.down, distance);


   


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
