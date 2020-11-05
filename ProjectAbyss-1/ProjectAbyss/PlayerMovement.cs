using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
    float HorizontalMove = 0f;
    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        // GetComponent<Grab>().isHolding = true;
        
            
            HorizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
       
        //animator.SetFloat("Speed",Mathf.Abs(HorizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jumped");
            jump = true;
            animator.SetBool("IsJumping", true);
        }
       /* if (GetComponent<Grab>().isHolding == true)
        {
            animator.SetBool("IsGrabbing", true);
        }
        else
        {
            animator.SetBool("IsGrabbing", false);
        }
        */
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    private void FixedUpdate()
    {
        //move character
        controller.Move(HorizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
