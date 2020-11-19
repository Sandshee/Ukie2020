using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Character following;
    public float speed = 0.1f;

    public float minDist = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (following)
        {
            float newX = Mathf.Lerp(transform.position.x, following.transform.position.x, speed);
            float newY = Mathf.Lerp(transform.position.y, following.transform.position.y, speed);
            float zValue = transform.position.z;

            Vector2 newVec = new Vector2(newX, newY);

            if (Vector2.Distance(newVec, following.transform.position) < minDist)
            {
                transform.position = new Vector3(following.transform.position.x, following.transform.position.y, zValue);
            }
            else
            {
                transform.position = new Vector3(newX, newY, zValue);
            }
        }
    }

    public void updateFollowing(Character following)
    {
        if (this.following)
        {
            this.following.deActivate();
        }
        this.following = following;
        this.following.activate();
    }
}
