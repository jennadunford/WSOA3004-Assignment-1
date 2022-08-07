using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Collsions
{
    public class Obsticle : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
         
            Debug.Log("Player in" + collision.collider.tag);
        }
    }
}

