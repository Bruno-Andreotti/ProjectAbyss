using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public float deltaY = 1.5f;  // quantidade para se mover pra cima e baixo do ponto de inicio
    public float speedY = 2.0f;

    public float deltaX = 1.5f;  // quantidade para se mover pra esquerda e direita do ponto de inicio
    public float speedX = 2.0f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        //código simples de movimentação de vai-e-vem, seria usado para plataformas móveis ou inimigos mais simples e de aparencia simétrica
        Vector3 v = startPos;
        v.x += deltaX * Mathf.Sin(Time.time * speedX);
        v.y += deltaY * Mathf.Sin(Time.time * speedY);
        transform.position = v;
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.transform.SetParent(this.transform);

    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.transform.SetParent(null);

    //    }
    //}
}

