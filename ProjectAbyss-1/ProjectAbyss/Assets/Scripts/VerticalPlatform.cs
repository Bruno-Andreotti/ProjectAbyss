using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {

        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotaciona o effector para que o jogador possa atravessar o colisor segurando a tecla de agachar, ou quando sobe uma escada
        if(Input.GetKeyUp(KeyCode.S))
        {
            waitTime = 0.5f;
        }

        if(Input.GetKey(KeyCode.S))
        {
            if(waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if(Input.GetKey(KeyCode.W))
        {
            effector.rotationalOffset = 0;
        }
    }
}
