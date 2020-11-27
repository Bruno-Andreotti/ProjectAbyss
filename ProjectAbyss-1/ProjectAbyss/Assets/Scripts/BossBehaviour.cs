using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossBehaviour : MonoBehaviour
{
    public float speed;
    public float distance;

    public int health = 1000;
    public int atkDamage; //sugiro também colocar já editável no inspetor e alterar no prefab os danos que cada inimigo deve causar. Seria interessante setar 2 valores diferentes como "Range" de dano, eventualmente 
    public float atkCD;   //Tempo de "recarga" (em segundos) entre ataques de cada inimigo
    bool vulnerable = false;
    public GameObject tentacle1;
    public GameObject tentacle2;
    public GameObject tentacle3;
    public GameObject tentacle4;

    public Transform limitDetection;
    public Animator bossAnim;
    

    public enum State
    {
        Intro,
        Chasing,
        Stopped
    }
    public State state;
    // Start is called before the first frame update
    void Start()
    {
        ChangeState();
    }
    IEnumerator Intro()
    {
        //aqui eu devo colocar um codigo que faz o boss fazer uma animação por uns 3 a 5 segundos, em que ele está completamente invencivel,
        //e talvez dê dano de contato. depois disso ele deve mudar pro proximo estado.

        FindObjectOfType<AudioManager>().Play("BossRoar"); //Assim que entra neste estado toca uma vez o rugido do Boss
        Invoke("introDelay", 3.5f); //Tempo de delay até o bos realmente começar e fazer alguma coisa
        bossAnim.SetBool("isMoving", false);

        while (state == State.Intro)
        {
           
            yield return new WaitForFixedUpdate();
        }
        ChangeState();
    }
    IEnumerator Chasing()
    {
        //aqui, o boss deve começar a se mover para a esquerda em uma velocidade constante, mais lento que o player é capaz de se mover,
        //mas ainda rapido o suficiente para ser ameaçador. Talvez a mecanica de tentaculos se esticando para atacar esteja nesse estado tambem,
        // e talvez ja seja possivel causar dano no boss.
       
        while (state == State.Chasing)
        {
            //transform.Translate(Vector2.left * speed * Time.deltaTime);
            bossAnim.SetBool("isMoving", true);
            
            
                yield return new WaitForFixedUpdate();
        }
        ChangeState();
    }
    IEnumerator Stopped()
    {
        //aqui, o boss fica parado, e deve entrar nesse estado quando entra no ultimo(primeiro) vagão. Ele deve conseguir atacar esticando tentaculos,
        //de modo que é possivel desviar agachando ou pulando. Dano de contato ainda deve ser possivel, e o boss tambem deve estar vulneravel a tiros.
        while (state == State.Stopped)
        {
            vulnerable = true;
            yield return new WaitForFixedUpdate();
        }
        ChangeState();
    }



    void ChangeState()
    {
        // StopAllCoroutines();
        StartCoroutine(state.ToString());
    }
    void ChangeState(State mystate)
    {
        state = mystate;
        //StopAllCoroutines();
        StartCoroutine(mystate.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        if(state == State.Chasing)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            RaycastHit2D groundInfo = Physics2D.Raycast(limitDetection.position, Vector2.down, distance);
            if (groundInfo.collider.CompareTag("BossLimit") == true)
            {
                Debug.Log("bosslimit");
                ChangeState(State.Stopped);
                //yield return new WaitForFixedUpdate();
            }
        }
    }

    void introDelay()
    {
        ChangeState(State.Chasing);
    }
    public void TakeDamage(int damage)
    {
        if (vulnerable == true)
        {
            health -= damage;

            if (health <= 0)
            {
                Die();
            }
        }

    }
    void Die()
    {
        Destroy(gameObject);
    }

}
