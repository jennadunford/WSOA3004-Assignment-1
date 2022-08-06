using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionChecker : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision.otherCollider.GetComponent<>
        Debug.Log("Collided");
    }
}
