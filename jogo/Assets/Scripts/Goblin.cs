using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    public Transform point;

    private Rigidbody2D rig;
    private Vector2 direction;

    public float speed;
    public float maxVision;

    //controladores
    private bool isFront;
    public bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove()
    {
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
                Debug.Log("Vendo player...");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(point.position, direction * maxVision);
    }
}
