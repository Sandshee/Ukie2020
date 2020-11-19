using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isAsleep;
    private SceneLoader sl;
    private Rigidbody2D rb2d;
    public float maxSpeed;
    public float force;

    public LayerMask whatIsGround;

    public int headingRight = 1;

    private SpriteRenderer sr;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        sl = FindObjectOfType<SceneLoader>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAsleep)
        {
            if (Mathf.Abs(rb2d.velocity.x) < maxSpeed)
            {
                rb2d.AddForce(force * headingRight * Vector2.right);
            }

            anim.SetBool("Sleeping", false);

        } else
        {
            anim.SetBool("Sleeping", true);
        }

        if (foundWall())
        {
            headingRight *= -1;
            sr.flipX = !sr.flipX;
            rb2d.velocity = Vector2.zero;
        }

        isAsleep = false;
    }

    public void sleep()
    {
        isAsleep = true;
    }

    private bool foundWall()
    {
        return Physics2D.Raycast(transform.position, headingRight * Vector2.right, 1f, whatIsGround);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAsleep)
        {
            sl.RestartScene();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isAsleep)
        {
            sl.RestartScene();
        }
    }

}
