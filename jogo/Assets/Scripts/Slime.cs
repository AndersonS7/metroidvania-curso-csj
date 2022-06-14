using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public Transform point;
    public LayerMask layer;

    public float radius;
    public float speed;
    public float health;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnCollision();
    }

    void FixedUpdate()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);
    }

    void OnCollision()
    {
        Collider2D hit = Physics2D.OverlapCircle(point.position, radius, layer);

        if (hit != null)
        {
            speed = -speed;

            if (transform.eulerAngles.y == 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    public void OnHit()
    {
        anim.SetTrigger("hit");

        health--;

        if (health <= 0)
        {
            speed = 0;
            anim.SetTrigger("dead");
            Destroy(gameObject, 1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }
}
