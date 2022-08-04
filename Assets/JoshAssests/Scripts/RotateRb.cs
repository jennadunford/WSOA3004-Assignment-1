using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Rotates a riged body depending on its velocity
public class RotateRb : MonoBehaviour
{
    private Rigidbody2D rb;
    private readonly Vector3 refLine = new Vector3(1, 0, 0);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float angle = Vector3.Angle(refLine, rb.velocity);
        rb.MoveRotation(angle);

    }
}
