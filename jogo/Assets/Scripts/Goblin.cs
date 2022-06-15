using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    public Transform point;
    public Transform behind;

    private Animator anim;
    private Rigidbody2D rig;
    private Vector2 direction;

    public float health;
    public float speed;
    public float maxVision;
    public float stopDistance;

    //controladores
    private bool isFront;
    private bool isRight;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        isRight = true;

        if (isRight)
        {
            transform.eulerAngles = new Vector2(0, 0);
            direction = Vector2.right;
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
            direction = Vector2.left;
        }
    }

    void OnMove()
    {
        if (isFront && !isDead)
        {
            anim.SetInteger("transition", 1);
            if (isRight)
            {
                transform.eulerAngles = new Vector2(0, 0);
                direction = Vector2.right;
                rig.velocity = new Vector2(speed, rig.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
                direction = Vector2.left;
                rig.velocity = new Vector2(-speed, rig.velocity.y);
            }
        }
    }

    void FixedUpdate()
    {
        GetPlayer();

        OnMove();
    }

    public void OnHit()
    {
        anim.SetTrigger("hit");

        health--;

        if (health <= 0)
        {
            isDead = true;
            speed = 0;
            anim.SetTrigger("dead");
            Destroy(gameObject, 1f);
        }
    }

    void GetPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, direction, maxVision);

        if (hit.collider != null && !isDead)
        {
            if (hit.transform.CompareTag("Player"))
            {
                isFront = true;

                float distance = Vector2.Distance(transform.position, hit.transform.position);

                if (distance <= stopDistance)
                {
                    isFront = false;
                    rig.velocity = Vector2.zero;

                    anim.SetInteger("transition", 2);

                    hit.transform.gameObject.GetComponent<Player>().OnHit();
                }
            }
        }

        RaycastHit2D behindeHit = Physics2D.Raycast(behind.position, -direction, maxVision);

        if (behindeHit.collider != null)
        {
            if (behindeHit.transform.CompareTag("Player"))
            {
                isRight = !isRight;
                isFront = true;
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawRay(point.position, direction * maxVision);
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawRay(bihide.position, -direction * maxVision);
    //}
}
