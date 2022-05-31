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
    private bool isGrounded;

    private float direction;
    private Vector2 facingRight;
    private Vector2 facingLeft;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
    }

    private void FixedUpdate()
    {
        Walking();

        Fight();
    }

    private void Update()
    {
        DoubleJump();
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpSpeed;
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


    private void Walking()
    {
        direction = (Input.GetAxisRaw("Horizontal"));


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
