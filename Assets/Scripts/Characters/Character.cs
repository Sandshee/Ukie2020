using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D rb2d;

    public bool active;
    public int activeZ;
    public int inactiveZ;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (active)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.AddForce(movement * movementSpeed);
        }
    }

    public void updateZ(float newZ)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    public void activate()
    {
        updateZ(activeZ);
        active = true;
    }

    public void deActivate()
    {
        updateZ(inactiveZ);
        active = false;
    }
}
