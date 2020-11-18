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
    private BoxCollider2D thisCol;

    public int normalLayer;
    public int frozenLayer;

    private bool onCoolDown = false;
    public int coolDown = 15;
    private int timer = 0;

    private void Start()
    {
        thisChar = GetComponent<Character>();
        thisBody = GetComponent<Rigidbody2D>();
        thisCol = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        ToggleSpringPower();
    }

    public void ToggleSpringPower()
    {
        if (!onCoolDown && thisChar.active)
        {
            if (Input.GetAxisRaw("Ability") > 0)
            {
                isSpringActive = !isSpringActive;
                SetSpringState();
            }

            onCoolDown = true;
        }

        if (onCoolDown)
        {
            timer++;
        }

        if (timer >= coolDown)
        {
            onCoolDown = false;
        }
    }

    public void SetSpringState() 
    {
        if (isSpringActive)
        {
            thisChar.freeze();
            isSpringActive = true;
            thisCol.isTrigger = true;
            thisBody.bodyType = RigidbodyType2D.Static;
            gameObject.layer = frozenLayer;

        }
        else
        {
            thisChar.unFreeze();
            isSpringActive = false;
            thisCol.isTrigger = false;
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
            if (colBody.velocity.y < 0)
            {
                colBody.velocity = new Vector3(colBody.velocity.x, springForce);
            } else
            {
                //Do nothing
            }
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, springForce));
        }
    }

}
