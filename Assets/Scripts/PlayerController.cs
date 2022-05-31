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
    private int jumpSpeed;

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

        Jump();

        Fight();
    }

    private void Fight()
    {
        
    }

    private void Jump()
    {
       
    }

    private void Walking()
    {
        direction = (Input.GetAxis("Horizontal"));

        if (direction > 0)
        {
            transform.localScale = facingRight;
        }

        if (direction < 0)
        {
            transform.localScale = facingLeft;
        }



        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);


    }
}
