using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void open()
    {
        anim.SetBool("Up", true);
        coll.enabled = false;
    }

    public void close()
    {
        anim.SetBool("Up", false);
        coll.enabled = true;
    }
}
