using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController2D controller;
        public Animator animator;

        public float runSpeed = 40f;
        float HorizontalMove = 0f;
        bool jump = false;
        int jumpCount = 1;

        // Update is called once per frame
        void Update()
        {
            // GetComponent<Grab>().isHolding = true;
            HorizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(HorizontalMove));

            if (Input.GetButtonDown("Jump") && jumpCount >= 1)
            {
                
                jump = true;
                jumpCount -= 2;
                //animator.SetBool("IsJumping", true);
                
            }
            /*
            if (GetComponent<Grab>().isHolding == true)
            {
                animator.SetBool("IsGrabbing", true);
            }
            else
            {
                animator.SetBool("IsGrabbing", false);
            }*/
        }

        public void OnLanding()
        {
            animator.SetBool("IsJumping", false);

            jumpCount = 1;
            
        }
        private void FixedUpdate()
        {
            //move character
            controller.Move(HorizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
            if(GetComponent<CharacterController2D>().m_Grounded == false)
            {
                jumpCount = 0;
            }
            
        }
    }
}