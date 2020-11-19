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

    public int defaultLayer;
    public int sleepingLayer;

    public LayerMask whatIsGround;

    public int headingRight = 1;

    private SpriteRenderer sr;

    public AudioClip sleeping;
    public AudioClip idle;
    private AudioSource audioS;

    private Animator anim;

    public int sleepLag;
    private int sleepTimer;

    // Start is called before the first frame update
    void Start()
    {
        sl = FindObjectOfType<SceneLoader>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        audioS = GetComponent<AudioSource>();
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
            audioS.clip = idle;
            gameObject.layer = defaultLayer;

        } else
        {
            anim.SetBool("Sleeping", true);
            audioS.clip = sleeping;
            gameObject.layer = sleepingLayer;
        }

        if (foundWall())
        {
            headingRight *= -1;
            sr.flipX = !sr.flipX;
            rb2d.velocity = Vector2.zero;
        }

        sleepTimer++;

        if(sleepTimer >= sleepLag)
        {
            isAsleep = false;
            sleepTimer = 0;
        }
    }

    public void sleep()
    {
        isAsleep = true;
        sleepTimer = 0;
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
