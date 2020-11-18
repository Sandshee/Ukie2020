using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float movementSpeed;
    private float regularSpeed;
    private Rigidbody2D rb2d;
    public float currentMovementSpeed;

    public bool active;
    public int activeZ;
    public int inactiveZ;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        regularSpeed = currentMovementSpeed;
    }

    void FixedUpdate()
    {
        if (active)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.AddForce(movement * currentMovementSpeed);
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

    public void freeze()
    {
        currentMovementSpeed = 0;
    }

    public void unFreeze()
    {
        currentMovementSpeed = regularSpeed;
    }
}
