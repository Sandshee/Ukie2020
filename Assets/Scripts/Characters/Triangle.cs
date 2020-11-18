using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    public EdgeCollider2D triLeft;
    public EdgeCollider2D triRight;
    private BoxCollider2D thisBox;
    private Rigidbody2D thisBody;
    private Character thisChar;

    public LayerMask normalLayer;
    public LayerMask frozenLayer;

    public bool faceLeft;

    public bool stopped = false;

    private bool onCoolDown = false;
    public int coolDown;
    private int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        thisBox = GetComponent<BoxCollider2D>();
        thisBody = GetComponent<Rigidbody2D>();
        thisChar = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Ability") > 0 && !onCoolDown)
        {
            onCoolDown = true;

            if (stopped)
            {
                stopped = false;
                thisChar.unFreeze();
                triLeft.enabled = false;
                triRight.enabled = false;
                thisBox.enabled = true;

                gameObject.layer = normalLayer;

            } else
            {
                stopped = true;
                thisChar.freeze();
                thisBox.enabled = false;

                gameObject.layer = frozenLayer;

                //If its facing left, enable the left block
                if (faceLeft)
                {
                    triLeft.enabled = true;
                } else
                {
                    triRight.enabled = true;
                }

            }
        }

        if(timer >= coolDown)
        {
            timer = 0;
            onCoolDown = false;
        }

        if (onCoolDown)
        {
            timer++;
        }

    }

    private void FixedUpdate()
    {
        if (stopped)
        {
            thisBody.AddForce(-thisBody.velocity);
        }
    }
}
