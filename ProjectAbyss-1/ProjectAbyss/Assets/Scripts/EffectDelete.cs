using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDelete : MonoBehaviour
{
    public float duration = 5.0f;
    

    // Update is called once per frame
    void Update()
    {
        Invoke("SelfDelete", duration);
    }
    void SelfDelete()
    {
        Destroy(this.gameObject);
    }
}
