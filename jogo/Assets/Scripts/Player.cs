using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public Transform point;
    public LayerMask enemyLayer;

    public float speed;
    public float jumpForce;
    public float radius;
    public float health;

    //controladores
    private bool isJumping;
    private bool doubleJump;
    private bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Attack();
    }

    void FixedUpdate()
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

            if (!isJumping && !isAttack)
            {
                anim.SetInteger("transition", 1);
            }
        }

        if (movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

            if (!isJumping && !isAttack)
            {
                anim.SetInteger("transition", 1);
            }
        }

        //player parado 
        if (movement == 0 && !isJumping && !isAttack)
        {
            anim.SetInteger("transition", 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                doubleJump = true;
                anim.SetInteger("transition", 2);
            }
            else if (doubleJump)
            {
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                doubleJump = false;
                anim.SetInteger("transition", 2);
            }
        }
    }

    void Attack()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            isAttack = true;
            anim.SetInteger("transition", 3);
            Collider2D hit = Physics2D.OverlapCircle(point.position, radius, enemyLayer);

            if (hit != null)
            {
                hit.GetComponent<Slime>().OnHit();
            }

            StartCoroutine(OnAttack());
        }
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(0.33f);
        isAttack = false;
    }

    void OnHit()
    {
        anim.SetTrigger("hit");
        health--;

        if (health <= 0)
        {
            anim.SetTrigger("dead");
            //game over
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            OnHit();
        }
    }
}
