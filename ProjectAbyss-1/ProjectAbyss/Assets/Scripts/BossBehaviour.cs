using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BossBehaviour : MonoBehaviour
{
    public float speed;
    public float distance;

    //private float rndtime;
    private float rndhit = 1;
    private float rndtime = 0;
    
    public int health = 1000;
    public int atkDamage; //sugiro também colocar já editável no inspetor e alterar no prefab os danos que cada inimigo deve causar. Seria interessante setar 2 valores diferentes como "Range" de dano, eventualmente 
    public float atkCD;   //Tempo de "recarga" (em segundos) entre ataques de cada inimigo
    private bool emRecarga = false;
    private bool vulnerable = false;
    private bool retornar; //Manda o tentáculo retornar

    public GameObject tentacle1;
    public GameObject tentacle2;
    public GameObject tentacle3;
    public GameObject tentacle4;
    private GameObject activeTentacle; //Variável que vai sempre mover o tentáculo ativo, assim podemos simplesmente alternar entre tentáculos

    public float tentacleSpeed;   

    public Transform limitDetection;
    public Transform tentacleLimit1;
    public Transform tentacleLimit2;
    public Transform tentacleLimitDetection1;
    public Transform tentacleLimitDetection2;
    // public Transform tentacleLimitDetection3;
    // public Transform tentacleLimitDetection4;

    private Transform tentacleLimitDetection;

    public Animator bossAnim;

    private GameObject origem1;
    private GameObject origem2;
    private GameObject origemalvo = null;

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
        origem1 = new GameObject("origem1");
        origem2 = new GameObject("origem2");
        
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
        

        origem1.transform.position = tentacle1.transform.position;
        origem2.transform.position = tentacle2.transform.position;

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
                RaycastHit2D groundInfo1 = Physics2D.Raycast(limitDetection.position, Vector2.down, distance);
                if (groundInfo1.collider.CompareTag("BossLimit"))
                {
                    ChangeState(State.Stopped);
                }


                break;

            case State.Stopped:
                
                Attack();

                if(activeTentacle != null && retornar == false)
                {                   
                   activeTentacle.transform.Translate(Vector2.left * tentacleSpeed * Time.deltaTime);
                   RaycastHit2D groundInfo2 = Physics2D.Raycast(tentacleLimitDetection.position, Vector2.down, distance);
                    if (groundInfo2.collider.CompareTag("Limiter"))
                    {
                       retornar = true;
                    }
                }

                if(retornar)
                {
                    activeTentacle.transform.position = Vector3.MoveTowards(activeTentacle.transform.position, origemalvo.transform.position, tentacleSpeed * Time.deltaTime);
                    //activeTentacle.transform.Translate(Vector2.right * tentacleSpeed * Time.deltaTime);

                    if (activeTentacle.transform.position == origem1.transform.position || activeTentacle.transform.position == origem2.transform.position)
                     {
                        retornar = false;
                        
                    }
                }

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
        
        if (emRecarga == false && retornar == false) //se não estiver em recarga e não tiver nenhum tentaculo retornando
        {
            rndhit = Random.Range(1, 3);
            rndtime = Random.Range(2, 6);


            Debug.Log("LOOOOOOOGG");
            
            if (rndhit <= 1)
            {
                
                activeTentacle = tentacle1;
                tentacleLimitDetection = tentacleLimitDetection1;
                origemalvo = origem1;

            }
            else if (rndhit > 1)
            {
                activeTentacle = tentacle2;
                tentacleLimitDetection = tentacleLimitDetection2;
                origemalvo = origem2;
            }

            atkCD = rndtime;
            emRecarga = true;
            Invoke("Recarregar", atkCD);
        }

    }
    void Recarregar()
    {

        emRecarga = false;
    }

   

}
