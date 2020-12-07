using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLimiter : MonoBehaviour
{
    private BossBehaviour boss;



     void OnTriggerEnter2D(Collider2D collision)
    {
        //esse codigo apenas serve para passar o estado do boss para o Stopped, quando este encostar no colisor do objeto associado.
        if (collision.gameObject.CompareTag("Boss"))
        {
            boss = collision.GetComponent<BossBehaviour>();
            boss.state = BossBehaviour.State.Stopped;
        }
    }
}
