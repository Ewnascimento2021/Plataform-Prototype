using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElevador : MonoBehaviour
{
    private Rigidbody2D rb;
    private float positionMax = 8.5f;
    private float positionMin = 1.55f;
    private float startPosX = 18f;
    private float startPosY = 1.7f;
    private bool testDown = true;

    [SerializeField]
    private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(startPosX, startPosY);
    }


    private void FixedUpdate()
    {
        UpDown();
    }

    private void UpDown()
    {
        if (testDown == true)
        {
            rb.MovePosition(rb.position + new Vector2(0, speed * 1));

            if (rb.position.y + speed * 1 >= positionMax)
            {
                testDown = false;
            }
        }
        if (testDown == false)
        {
            rb.MovePosition(rb.position + new Vector2(0, speed * -1));

            if (rb.position.y + speed * -1 <= positionMin)
            {
                testDown = true;
            }
        }
    }
}
