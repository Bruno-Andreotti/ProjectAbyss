using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDelete : MonoBehaviour
{
    public float duration = 5.0f;
    

    // Update is called once per frame
    void Update()
    {
        //faz com que efeitos especiais, como jorras de sangue, desapareçam depois da duração, para nao pesar a cena com um monte de objetos
        Invoke("SelfDelete", duration);
    }
    void SelfDelete()
    {
        Destroy(this.gameObject);
    }
}
