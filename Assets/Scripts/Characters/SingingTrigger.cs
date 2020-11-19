using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingingTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        //change to tag if you prefer :D
        if(collision.gameObject.name=="Enemy")
        PutEnemiesToSleep(collision.gameObject);
    }

    public void PutEnemiesToSleep(GameObject enemy) 
    {
        enemy.GetComponent<Enemy>().isAsleep = true;
    }
}
