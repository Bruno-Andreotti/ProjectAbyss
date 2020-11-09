using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSelect : MonoBehaviour
{
    public float NPCID;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("NPC", NPCID);
    }

    // Update is called once per frame
    
}
