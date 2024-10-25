using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float Jumpforce;
    public bool isJumping;
    public bool DoubleJump;
    private Rigidbody2D rig;

    void Start()
    {
     
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        float movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
     void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig.AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
                isJumping = true;
                DoubleJump = true;

            }
            else if (DoubleJump)
            {
                rig.AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
                DoubleJump = false;
            }
        }
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
        }
    }
}
