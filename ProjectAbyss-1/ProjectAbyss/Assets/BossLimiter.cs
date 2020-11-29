using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLimiter : MonoBehaviour
{
    private BossBehaviour boss;



     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            boss = collision.GetComponent<BossBehaviour>();
            boss.state = BossBehaviour.State.Stopped;
        }
    }
}
