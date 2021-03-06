﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    public EdgeCollider2D triLeft;
    public EdgeCollider2D triRight;
    private CapsuleCollider2D thisCap;
    private Rigidbody2D thisBody;
    private Character thisChar;
    public Animator anim;

    private AudioSource audioS;
    public AudioClip sigh;

    public int normalLayer;
    public int frozenLayer;

    public bool faceLeft;

    public bool stopped = false;

    private bool onCoolDown = false;
    public int coolDown;
    public int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        thisCap = GetComponent<CapsuleCollider2D>();
        thisBody = GetComponent<Rigidbody2D>();
        thisChar = GetComponent<Character>();
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(thisBody.velocity.x > 1f)
        {
            faceLeft = false;
        } else if(thisBody.velocity.x < -1f)
        {
            faceLeft = true;
        }

        if (Input.GetAxisRaw("Ability") > 0 && !onCoolDown && thisChar.active)
        {
            onCoolDown = true;

            if (stopped)
            {
                stopped = false;
                thisChar.unFreeze();
                triLeft.enabled = false;
                triRight.enabled = false;
                thisCap.enabled = true;

                gameObject.layer = normalLayer;
                thisBody.bodyType = RigidbodyType2D.Dynamic;

                anim.SetBool("Sitting", false);
            }
            else
            {
                audioS.PlayOneShot(sigh);
                stopped = true;
                thisChar.freeze();
                thisCap.enabled = false;
                gameObject.layer = frozenLayer;
                thisBody.bodyType = RigidbodyType2D.Static;

                anim.SetBool("Sitting", true);

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

        if (onCoolDown && Input.GetAxisRaw("Ability") <= 0)
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
