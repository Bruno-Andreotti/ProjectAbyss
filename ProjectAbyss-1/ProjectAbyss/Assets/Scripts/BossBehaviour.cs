using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossBehaviour : MonoBehaviour
{
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

    }
}
