using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent onButtonPress;
    public UnityEvent onButtonLeave;

    public int requiredMass = 0;
    private int currentlyOn = 0;

    private bool on = false;

    private Animator anim;
    private AudioSource audioS;
    public AudioClip press;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        audioS = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (on)
        {
            if (currentlyOn < requiredMass)
            {
                on = false;
                onButtonLeave.Invoke();

                anim.SetBool("On", false);
                audioS.PlayOneShot(press);
            }
        } else
        {
            if (currentlyOn >= requiredMass)
            {
                on = true;
                onButtonPress.Invoke();

                anim.SetBool("On", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentlyOn++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentlyOn--;
        }
    }

}
