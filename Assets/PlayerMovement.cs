using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    AnimatorStateInfo animatorState;
    
    public float walkSpeed = 20f;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool isJumping = false;

    float speed = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;
        speed = Mathf.Abs(horizontalMove);
        animator.SetFloat("Speed", speed);
        animator.SetBool("isMoving", speed > 0);
        animator.SetBool("isWalking", speed > 0);

        if (Input.GetButtonDown("Jump")) {
            animator.SetBool("isJumping", true);
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, false, isJumping);
        isJumping = false;
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }
}
