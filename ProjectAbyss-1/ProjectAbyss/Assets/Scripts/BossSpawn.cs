using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject boss;
    private BossBehaviour bossB;
    public Transform bossSpawn;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {//em colisão com o player, spawna o boss, e o coloca no estado de Intro
        if (collision.collider.CompareTag("Player1") == true)
        {
            Instantiate(boss, bossSpawn.position, bossSpawn.rotation);
            bossB = boss.GetComponent<BossBehaviour>();
            bossB.state = BossBehaviour.State.Intro;

        }
    }
}
