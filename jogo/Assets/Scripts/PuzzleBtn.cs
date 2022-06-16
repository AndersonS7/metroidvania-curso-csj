using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBtn : MonoBehaviour
{
    private Animator btnAnim;

    public LayerMask layer;
    public Animator barrieAnim;

    void OnPressed()
    {
        btnAnim.SetBool("isPressed", true);
        barrieAnim.SetBool("isPressed", true);
    }

    void Start()
    {
        btnAnim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        OnCollision();
    }

    void OnExit()
    {
        btnAnim.SetBool("isPressed", false);
        barrieAnim.SetBool("isPressed", false);
    }

    void OnCollision()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.3f, layer);

        if (hit != null)
        {
            OnPressed();
        }
        else
        {
            OnExit();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}
