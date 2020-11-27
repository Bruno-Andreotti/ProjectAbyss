﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject boss;
    public Transform bossSpawn;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player1") == true)
        {
            Instantiate(boss, bossSpawn.position, bossSpawn.rotation);
        }
    }
}
