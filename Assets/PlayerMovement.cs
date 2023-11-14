using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public PlayerInputs playerInputs;
    private InputAction move;
    private InputAction fire;
    private InputAction sprint;
    
    public float walkSpeed = 20f;
    public float runSpeed = 40f;
    float groundSpeed;

    bool isJumping = false;
    float speed = 0f;
    Vector2 moveDirection = Vector2.zero;


    private void Awake()
    {
        playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        move = playerInputs.Player.Move;
        sprint = playerInputs.Player.Sprint;
        move.Enable();
        sprint.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        speed = moveDirection.magnitude;
        animator.SetFloat("Speed", speed);
        animator.SetBool("isMoving", speed > 0);
        animator.SetBool("isWalking", speed > 0);

        if (moveDirection.y > 0) {
            animator.SetBool("isJumping", true);
            isJumping = true;
        }

        if (sprint.IsPressed())
        {
            animator.SetBool("isRunning", true);
            groundSpeed = runSpeed;
        } else
        {
            animator.SetBool("isRunning", false);
            groundSpeed = walkSpeed;
        }
    }

    void FixedUpdate()
    {
        controller.Move(moveDirection.x * groundSpeed * Time.deltaTime, false, isJumping);
        isJumping = false;
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }
}
