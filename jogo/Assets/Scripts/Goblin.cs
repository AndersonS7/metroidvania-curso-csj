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

    public float speed;
    public float maxVision;
    public float stopDistance;

    //controladores
    private bool isFront;
    public bool isRight;

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
        if (isFront)
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

    void GetPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, direction, maxVision);

        if (hit.collider != null)
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
