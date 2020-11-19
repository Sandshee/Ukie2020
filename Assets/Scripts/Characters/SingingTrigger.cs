using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingingTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        //change to tag if you prefer :D
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().sleep();
        }
    }
}
