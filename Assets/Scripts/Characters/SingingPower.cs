using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingingPower : MonoBehaviour
{
    public bool canUseSingingPower;
    public bool isSingingActive;
    public SingingTrigger singingVisuals;
    public CircleCollider2D singingHitbox;

    public Animator singingAnim;
    public Animator visualsAnim;

    private bool onCoolDown = false;
    public int coolDown;
    public int timer = 0;

    public bool stopped;

    private Character thisChar;

    public AudioSource audioS;

    //Requires:
    //-a child game object to activate (SingingVisuals), this will display the sound visuals (waves, notes or whatever effect), and will also hold a 2d collider component, set to trigger 
    //-the chiuld component should have the SingingTrigger script
    //-input tweak to || Input.GetAxisRaw("Ability")>0

    //How it works:
    //-while holding the Ability button, the character sings. 
    //-while singing is active, detect colliding game objects with a 2d collider component (enemies?)


    private void Start()
    {
        visualsAnim = singingVisuals.GetComponent<Animator>();
        singingHitbox = singingVisuals.GetComponent<CircleCollider2D>();
        thisChar = GetComponent<Character>();
    }

    void Update()
    {
        ToggleSingingPower();
    }

    public void ToggleSingingPower()
    {

        if (Input.GetAxisRaw("Ability") > 0 && !onCoolDown && thisChar.active)
        {
            onCoolDown = true;

            if (stopped)
            {
                audioS.Pause();
                stopped = false;
                thisChar.unFreeze();
                singingAnim.SetBool("Singing", false);

                singingHitbox.enabled = false;
                visualsAnim.SetBool("Singing", false);
            }
            else
            {
                audioS.Play();
                stopped = true;
                thisChar.freeze();
                singingAnim.SetBool("Singing", true);

                singingHitbox.enabled = true;
                visualsAnim.SetBool("Singing", true);

            }
        }

        if (timer >= coolDown)
        {
            timer = 0;
            onCoolDown = false;
        }

        if (onCoolDown && Input.GetAxisRaw("Ability") <= 0)
        {
            timer++;
        }
    }

}
