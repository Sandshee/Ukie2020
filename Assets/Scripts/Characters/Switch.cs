using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public FollowCam cam;
    private int index = 0;
    private Character[] characters;

    public int coolDown = 30;
    private int timer = 0;

    private bool canSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
        characters = FindObjectsOfType<Character>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Switch left and right
        if(canSwitch && Input.GetAxisRaw("SwitchLeft") > 0)
        {
            index--;
            canSwitch = false;
        } else if(canSwitch && Input.GetAxisRaw("SwitchRight") > 0)
        {
            index++;
            canSwitch = false;
        }


        if (!canSwitch)
        {
            timer++;
        }

        if(timer >= coolDown)
        {
            canSwitch = true;
            timer = 0;
        }

        //Loop round the list
        if(index < 0)
        {
            index = characters.Length - 1;
        } else if(index >= characters.Length)
        {
            index = 0;
        }

        cam.updateFollowing(characters[index]);
    }
}
