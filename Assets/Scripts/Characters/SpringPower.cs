using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPower : MonoBehaviour
{
    public bool canUseSpringPower;
    public bool isSpringActive;
    public float springForce;

    void Update()
    {
        ToggleSpringPower();
    }

    public void ToggleSpringPower()
    {
        if (canUseSpringPower)
        {
            //|| Input.GetAxisRaw("Ability")>0
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSpringActive = !isSpringActive;
                SetSpringState();
            }
        }
    }

    public void SetSpringState() 
    {
        if (isSpringActive)
        {
            this.gameObject.GetComponent<Character>().currentMovementSpeed = 0;
            isSpringActive = true;
        }
        else
        {
            this.gameObject.GetComponent<Character>().currentMovementSpeed = this.gameObject.GetComponent<Character>().movementSpeed;
            isSpringActive = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isSpringActive)
        {
            print(collision.gameObject.name);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2 (0f, springForce));
        }
    }


}
