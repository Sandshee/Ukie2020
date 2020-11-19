using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D coll;
    AudioSource audioS;
    public AudioClip slide;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void open()
    {
        anim.SetBool("Up", true);
        coll.enabled = false;
        audioS.PlayOneShot(slide);
    }

    public void close()
    {
        anim.SetBool("Up", false);
        coll.enabled = true;
        audioS.PlayOneShot(slide);

    }
}
