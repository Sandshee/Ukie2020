using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Character following;
    public float speed = 0.1f;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float newX = Mathf.Lerp(transform.position.x, following.transform.position.x, speed);
        float newY = Mathf.Lerp(transform.position.y, following.transform.position.y, speed);
        float zValue = transform.position.z;

        transform.position = new Vector3(newX, newY, zValue);
    }

    public void updateFollowing(Character following)
    {
        this.following = following;
    }
}
