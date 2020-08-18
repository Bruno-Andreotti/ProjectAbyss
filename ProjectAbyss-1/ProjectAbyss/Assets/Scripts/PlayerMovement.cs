using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
    float HorizontalMove = 0f;
    float jump = 0f;

    // Update is called once per frame
    void Update()
    {
        // GetComponent<Grab>().isHolding = true;
        HorizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed",Mathf.Abs(HorizontalMove));

        if(Input.GetAxisRaw("Vertical")> 0)
        {
            jump = 20f;
            animator.SetBool("IsJumping", true);
        }
        if (GetComponent<Grab>().isHolding == true)
        {
            animator.SetBool("IsGrabbing", true);
        }
        else
        {
            animator.SetBool("IsGrabbing", false);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    private void FixedUpdate()
    {
        //move character
        controller.Move(HorizontalMove * Time.fixedDeltaTime, false, jump);
        jump = 0f;
    }
}
