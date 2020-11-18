using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float movementSpeed;
    private float regularSpeed;
    private Rigidbody2D rb2d;
    public float currentMovementSpeed;

    public LayerMask whatIsGround;
    public float slopeFriction;

    public float maxSpeed;

    public bool active;
    public int activeZ;
    public int inactiveZ;
    private bool frozen;

    private SpriteRenderer sr;

    public float rayDist = 1f;
    public float offset = 0.5f;

    public bool grounded = false;

    private Animator anim;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        regularSpeed = currentMovementSpeed;
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (active)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            //float moveVertical = Input.GetAxis("Vertical");

            Vector2 direction = complexGround();

            Debug.DrawRay(transform.position, direction);

            if (Mathf.Abs(Vector2.Dot(rb2d.velocity, direction)) < maxSpeed)
            {
                rb2d.AddForce(direction * moveHorizontal * currentMovementSpeed);
            } else if (Vector2.Dot(rb2d.velocity, direction) < maxSpeed && moveHorizontal > 0)
            {
                rb2d.AddForce(direction * moveHorizontal * currentMovementSpeed);
            } else if (Vector2.Dot(rb2d.velocity, direction) > -maxSpeed && moveHorizontal < 0)
            {
                rb2d.AddForce(direction * moveHorizontal * currentMovementSpeed);
            }

            if(moveHorizontal == 0)
            {
                rb2d.AddForce(-direction * Vector2.Dot(rb2d.velocity, direction));
            }

            if(Mathf.Abs(Vector2.Dot(rb2d.velocity, direction)) > 0.5f)
            {
                anim.SetBool("Moving", true);
            } else
            {
                anim.SetBool("Moving", false);
            }
            rotation(direction);
        }

        if(rb2d.velocity.x > 0)
        {
            sr.flipX = false;
        } else if (rb2d.velocity.x < 0)
        {
            sr.flipX = true;
        }
    }

    private void rotation(Vector2 direction)
    {
        if (frozen)
        {
            return;
        }
        sr.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, direction)));
    }

    public Vector2 complexGround()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + Vector3.left * offset, -Vector2.up, rayDist, whatIsGround);
        Debug.DrawRay(transform.position + Vector3.left * offset, -Vector2.up);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + Vector3.right * offset, -Vector2.up, rayDist, whatIsGround);
        Debug.DrawRay(transform.position + Vector3.right * offset, -Vector2.up);

        Vector2 normLeft = Vector2.zero;
        Vector2 normRight = Vector2.zero;

        if (hitLeft.collider)
        {
            normLeft = hitLeft.normal;
        }

        if (hitRight.collider)
        {
            normRight = hitRight.normal;
        }

        if(normRight.sqrMagnitude == normLeft.sqrMagnitude && normRight.sqrMagnitude == 0)
        {
            grounded = false;
            return Vector2.zero;
        }

        return normToTang(normLeft + normRight);

    }

    private Vector2 normToTang(Vector2 norm)
    {
        Vector2 tang = new Vector2(norm.y, -norm.x);

        return tang.normalized;
    }

    void NormalizeSlope()
    {
        // Attempt vertical normalization
        if (isGrounded())
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, whatIsGround);

            if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f)
            {
                Rigidbody2D body = GetComponent<Rigidbody2D>();
                // Apply the opposite force against the slope force 
                // You will need to provide your own slopeFriction to stabalize movement
                body.velocity = new Vector2(body.velocity.x - (hit.normal.x * slopeFriction), body.velocity.y);

                //Move Player up or down to compensate for the slope below them
                Vector3 pos = transform.position;
                pos.y += -hit.normal.x * Mathf.Abs(body.velocity.x) * Time.deltaTime * (body.velocity.x - hit.normal.x > 0 ? 1 : -1);
                transform.position = pos;
            }
        }
    }

    public bool isGrounded()
    {
       return GetComponent<Rigidbody2D>().IsTouchingLayers(whatIsGround);
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
        frozen = true;
        currentMovementSpeed = 0;
    }

    public void unFreeze()
    {
        frozen = false;
        currentMovementSpeed = regularSpeed;
    }
}
