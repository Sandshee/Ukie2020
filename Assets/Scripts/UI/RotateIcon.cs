using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIcon : MonoBehaviour
{
    public Switch switchCont;

    public Vector3 startAngle;
    private Vector3 destAngle;
    private bool moving;

    public float[] angles = { 0, 90, 180, -90};
    private Quaternion[] quatAngles = new Quaternion[4];
    private float index;

    public float speed;


    // Update is called once per frame

    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            quatAngles[i] = Quaternion.Euler(new Vector3(0, 0, angles[i]));
        }

        transform.rotation = quatAngles[0];
    }

    void FixedUpdate()
    {

        if (switchCont.direction != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, quatAngles[switchCont.index], speed);
        }

    }
}
