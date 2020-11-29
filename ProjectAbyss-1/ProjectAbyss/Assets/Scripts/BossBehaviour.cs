using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BossBehaviour : MonoBehaviour
{
    public float speed;
    public float distance;

    //private float rndtime;
    private float rndhit = 1;
    public int health = 1000;
    public int atkDamage; //sugiro também colocar já editável no inspetor e alterar no prefab os danos que cada inimigo deve causar. Seria interessante setar 2 valores diferentes como "Range" de dano, eventualmente 
    public float atkCD;   //Tempo de "recarga" (em segundos) entre ataques de cada inimigo
    private bool emRecarga = false;
    bool vulnerable = false;
    public GameObject tentacle1;
    public GameObject tentacle2;
    public GameObject tentacle3;
    public GameObject tentacle4;

    public float tentacleSpeed;

    

    public Transform limitDetection;
    public Transform tentacleLimit1;
    public Transform tentacleLimit2;
    public Transform tentacleLimitDetection1;
    public Transform tentacleLimitDetection2;
    public Transform tentacleLimitDetection3;
    public Transform tentacleLimitDetection4;
    public Animator bossAnim;
    

    public enum State
    {
        Intro,
        Chasing,
        Stopped,
        DeathAnim,
        Dead
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
        Invoke("introDelay", 2.5f); //Tempo de delay até o bos realmente começar e fazer alguma coisa
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
    IEnumerator DeathAnim()
    {
        bossAnim.SetBool("IsDying", true);
            FindObjectOfType<AudioManager>().Play("BossRoar");
        Invoke("DeathDelay", 5f);
        
        while (state == State.DeathAnim)
        {
            
            yield return new WaitForFixedUpdate();
        }
        ChangeState();
    }
    IEnumerator Dead()
    {
        
        while (state == State.Dead)
        {
            Die();
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
        switch(state)
        {
            case State.Chasing:
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            RaycastHit2D groundInfo = Physics2D.Raycast(limitDetection.position, Vector2.down, distance);
            if (groundInfo.collider.CompareTag("BossLimit") == true)
            {
                Debug.Log("bosslimit");
                ChangeState(State.Stopped);
                //yield return new WaitForFixedUpdate();
            }
                break;
            case State.Stopped:
                
                //float rndtime;
                //float rndhit;

                //rndhit = Random.Range(1, 6);
                //rndtime = Random.Range(1, 6);
                Attack();
                Debug.Log(rndhit);
                break;
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
                ChangeState(State.DeathAnim);
            }
        }

    }
    void DeathDelay()
    {
        ChangeState(State.Dead);
    }
    void Die()
    {
        Destroy(gameObject);
    }
    void Attack()
    {
        
        if (emRecarga == false)
        {
            Debug.Log("LOOOOOOOGG");
            
            if (rndhit == 1)
            {
                Debug.Log("Tentaculos Movendo");
                tentacle1.transform.Translate(Vector2.left * tentacleSpeed * Time.deltaTime);
                tentacle4.transform.Translate(Vector2.left * tentacleSpeed * Time.deltaTime);
                RaycastHit2D groundInfo1 = Physics2D.Raycast(tentacleLimitDetection1.position, Vector2.down, distance);
                if (groundInfo1.collider.name == "TentacleLimit1")
                {
                    tentacle1.transform.Translate(Vector2.right * tentacleSpeed * Time.deltaTime);
                }

                RaycastHit2D groundInfo4 = Physics2D.Raycast(tentacleLimitDetection4.position, Vector2.down, distance);
                if (groundInfo4.collider.name == "TentacleLimit1")
                {
                    tentacle4.transform.Translate(Vector2.right * tentacleSpeed * Time.deltaTime);
                }
            }
            if (rndhit == 2)
            {
                Debug.Log("Tentaculos Movendo");
                tentacle3.transform.Translate(Vector2.left * tentacleSpeed * Time.deltaTime);
                tentacle2.transform.Translate(Vector2.left * tentacleSpeed * Time.deltaTime);
                RaycastHit2D groundInfo3 = Physics2D.Raycast(tentacleLimitDetection3.position, Vector2.down, distance);
                if (groundInfo3.collider.name == "TentacleLimit2")
                {
                    tentacle3.transform.Translate(Vector2.right * tentacleSpeed * Time.deltaTime);
                }

                RaycastHit2D groundInfo2 = Physics2D.Raycast(tentacleLimitDetection2.position, Vector2.down, distance);
                if (groundInfo2.collider.name == "TentacleLimit2")
                {
                    tentacle2.transform.Translate(Vector2.right * tentacleSpeed * Time.deltaTime);
                }
            }

            emRecarga = true;
            Invoke("Recarregar", atkCD);
        }

    }
    void Recarregar()
    {
        emRecarga = false;
    }
}
