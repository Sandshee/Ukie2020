using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingingPower : MonoBehaviour
{
    public bool canUseSingingPower;
    public bool isSingingActive;
    public GameObject SingingVisuals;

    //Requires:
    //-a child game object to activate (SingingVisuals), this will display the sound visuals (waves, notes or whatever effect), and will also hold a 2d collider component, set to trigger 
    //-the chiuld component should have the SingingTrigger script
    //-input tweak to || Input.GetAxisRaw("Ability")>0

    //How it works:
    //-while holding the Ability button, the character sings. 
    //-while singing is active, detect colliding game objects with a 2d collider component (enemies?)


    void Update()
    {
        ToggleSingingPower();
    }

    public void ToggleSingingPower()
    {
        if (canUseSingingPower)
        {
            //|| Input.GetAxisRaw("Ability")>0
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSingingActive = true;
                SetSpringState();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isSingingActive = false;
                SetSpringState();
            }
        }
    }

    public void SetSpringState()
    {
        if (isSingingActive)
        {
            SingingVisuals.SetActive(true);
        }
        else
        {
            SingingVisuals.SetActive(false);
        }
    }

}
