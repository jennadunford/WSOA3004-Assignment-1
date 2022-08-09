using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "obstacle")
        {
            Debug.Log("Entered obstacle");
        }
    }
}
