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

    public Transform point;
    public float radius;
    public bool isAttacking;

    public Animator anim;
    void Start()
    {
     
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Attack();
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
            if (!isJumping && !isAttacking)
            {
                anim.SetInteger("transition", 1);
            }
           
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (movement < 0 && !isAttacking)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
           
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if(movement == 0 && !isJumping && !isAttacking)
        {
            anim.SetInteger("transition", 0);
        }
    }
     void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
                isJumping = true;
                DoubleJump = true;

            }
            else if (DoubleJump)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
                DoubleJump = false;
            }
        }
    }
    void Attack()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
            anim.SetInteger("transition", 3);
            Collider2D hit = Physics2D.OverlapCircle(point.position, radius);

            if (hit != null)
            {
                Debug.Log(hit.name);
            }
            StartCoroutine(OnAttack());
        }

        

    }
    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
        }
    }
}
