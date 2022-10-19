using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform firePoint;
    private Animator animator;
    private Vector2 facingRight;
    private Vector2 facingLeft;
    public LayerMask raycastLayerMask;

    [SerializeField]
    private int moveSpeed;
    [SerializeField]
    private int doubleJump;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float counterJump;
    [SerializeField]
    private float reloadTimeAttack;
    [SerializeField]
    private float reloadTimeAttack2;
    [SerializeField]
    private float reloadTimeRoll;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private bool isRolling;

    private bool isBullet;
    private bool isPlatform;
    private bool testDefend;
    private float direction;
    public bool platform;


    RaycastHit2D hitInfo;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DoubleJump();

        //VariableJump();

        Attack1();

        Attack2();

        Roll();

        Defend();

        Run();

        JumpTest();

        hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up, 4f, raycastLayerMask);

        isPlatform = hitInfo;
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
                animator.SetBool("Jump", true);
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && doubleJump > 0)
            {
                doubleJump--;
                Jump();
                animator.SetBool("Jump", true);
            }
            if (isGrounded)
            {
                animator.SetBool("Jump", false);
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
        if (testDefend == false)
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
    }

    private void Attack1()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack1");
        }
    }

    private void Attack2()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Attack2");
        }
    }

    private void Defend()
    {
        {
            if (Input.GetButtonDown("Defend"))
            {
                animator.SetBool("Defend", true);

                testDefend = true;
            }
            if (Input.GetButtonUp("Defend"))
            {
                animator.SetBool("Defend", false);

                testDefend = false;
            }
        }
    }

    private void Roll()

    {
        if (Input.GetButtonDown("Roll"))
        {
            animator.SetBool("Roll", true);
        }
        if (isPlatform == true)
        {
            animator.SetBool("Roll", true);
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
            //Debug.DrawLine(firePoint.position, hitInfo.point, Color.red);
        }

        else if ((Input.GetAxisRaw("Roll") == 0) && (isPlatform == false))
        {
            {
                animator.SetBool("Roll", false);
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject)
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
        }
        if (other.CompareTag("Flecha"))
        {
            Destroy(other.gameObject);
            animator.SetTrigger("Damage");
            Debug.Log(isBullet);
        }
        if (other.CompareTag("Armadilha"))
        {
            animator.SetTrigger("Kill");
        }
        if (other.CompareTag("Bau"))
        {
            Destroy(other.gameObject);
        }


    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject)
        {
            isGrounded = false;
        }
    }
}