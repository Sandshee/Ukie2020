using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIcon : MonoBehaviour
{
    public Switch switchCont;

    public Vector3 startAngle;
    private Vector3 destAngle;
    private bool moving;

    public float angle;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (switchCont.direction != 0 && !moving)
        {
            startAngle = transform.rotation.eulerAngles;
            destAngle = new Vector3(0, 0, transform.rotation.eulerAngles.z + (angle * switchCont.direction));
            moving = true;
        }

        transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, destAngle, speed));

        if (switchCont.direction == 0)
        {
            moving = false;
        }

    }
}
