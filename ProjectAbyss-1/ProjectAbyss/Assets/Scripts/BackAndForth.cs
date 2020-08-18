using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public float deltaY = 1.5f;  // Amount to move left and right from the start point
    public float speedY = 2.0f;

    public float deltaX = 1.5f;  // Amount to move left and right from the start point
    public float speedX = 2.0f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
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

