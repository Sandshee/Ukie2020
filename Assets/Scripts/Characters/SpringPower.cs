using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPower : MonoBehaviour
{
    public bool canUseSpringPower;
    public bool isSpringActive;
    public float springForce;
    private Character thisChar;
    private Rigidbody2D thisBody;
    public CapsuleCollider2D thisCol;
    public BoxCollider2D springCol;
    private Animator anim;

    private AudioSource audioS;
    public AudioClip spring;

    public int normalLayer;
    public int frozenLayer;

    private bool onCoolDown = false;
    public int coolDown = 15;
    public int timer = 0;

    private void Start()
    {
        thisChar = GetComponent<Character>();
        thisBody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        audioS = GetComponent<AudioSource>();

    }

    void Update()
    {
        ToggleSpringPower();
    }

    public void ToggleSpringPower()
    {
        if (thisChar.active)
        {
            if (Input.GetAxisRaw("Ability") > 0 && !onCoolDown)
            {
                isSpringActive = !isSpringActive;
                SetSpringState();

                onCoolDown = true;
            }
        }

        if (onCoolDown && Input.GetAxisRaw("Ability") <= 0)
        {
            timer++;
        }

        if (timer >= coolDown)
        {
            onCoolDown = false;
            timer = 0;
        }
    }

    public void SetSpringState() 
    {
        if (isSpringActive)
        {
            anim.SetBool("Ready", true);

            thisChar.freeze();
            isSpringActive = true;
            thisCol.enabled = false;
            springCol.enabled = true;
            thisBody.bodyType = RigidbodyType2D.Static;
            gameObject.layer = frozenLayer;

        }
        else
        {
            anim.SetBool("Ready", false);

            thisChar.unFreeze();
            isSpringActive = false;
            thisCol.enabled = true;
            springCol.enabled = false;
            thisBody.bodyType = RigidbodyType2D.Dynamic;
            gameObject.layer = normalLayer;

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print(collision.gameObject.name);
            Rigidbody2D colBody = collision.GetComponent<Rigidbody2D>();
            //Ensures we only spring objects moving down.
            if (colBody.velocity.y < -1f)
            {
                colBody.velocity = new Vector3(colBody.velocity.x, springForce);
                Debug.Log("Badoing!");
                anim.SetTrigger("Boing");
                audioS.PlayOneShot(spring);
            } else
            {
                Debug.Log("No, not going down");
            }
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, springForce));
        }
    }

}
