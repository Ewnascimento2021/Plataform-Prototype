using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private int moveSpeed;
    [SerializeField]
    private int doubleJump;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float counterJump;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool isJumping;

    private Animator animator;

    private float direction;
    private Vector2 facingRight;
    private Vector2 facingLeft;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Run();

        JumpTest();

        Fight();
    }



    private void Update()
    {
        VariableJump();
    }

    private void Jump()
    {
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }


    private void DoubleJump()
    {
        if (isGrounded)
        {
            doubleJump = 1;

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && doubleJump > 0)
            {
                doubleJump--;
                Jump();
            }
        }
    }


    private void VariableJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            animator.SetBool("Jump", true);
        }
        if (Input.GetButton("Jump"))
        {
            counterJump -= Time.deltaTime;
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
           
            counterJump = 0.5f;
        }
    }

    private void JumpTest()
    {
        if (isJumping)
        {
            if (counterJump > 0)
            {
                Jump();
            }
            else
            {
                isJumping = false;
            }
        }
    }


    private void Run()
    {
        direction = (Input.GetAxisRaw("Horizontal"));
        if (direction == 0)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Run", true);
        }

        if (direction == 1)
        {
            transform.localScale = facingRight;
        }

        else if (direction == -1)
        {
            transform.localScale = facingLeft;
        }

        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }


    private void Fight()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
