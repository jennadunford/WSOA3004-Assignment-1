using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedRunnerCtrl : MonoBehaviour
{
    [SerializeField] PlayerBounds bounds;
    [SerializeField] private float forwardSpeed = 1f, shiftForce = 1f, outDrag = 1f, maxOutSpeed, outSpin = 10f;
    [SerializeField] string shiftAxis = "Horizontal";

    public bool IsRunning { get; private set; } = false;
    private bool outOfBound = false;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bounds.SetPlayer(rb);
        bounds.OnOutOfBounds.AddListener(OutOfBounds);
    }

    private void FixedUpdate()
    {
        if (IsRunning)
        {
            rb.AddForce(Input.GetAxis(shiftAxis) * bounds.GetInputCorrection() * shiftForce * bounds.Axese.rowVect);
            rb.velocity = bounds.Axese.RowComponentVect(rb.velocity) + bounds.Axese.colVect * forwardSpeed;
            bounds.CheckBounds();
        }
        if (outOfBound && bounds.Axese.ColComponent(rb.velocity) > -maxOutSpeed)
        {
            // Gradually deccelerate 
            rb.AddForce(-outDrag * bounds.Axese.colVect);
        }
        else
        {
            if (Input.anyKeyDown)
            {
                StartRunning();
            }
        }
    }


    private void OutOfBounds()
    {
        IsRunning = false;
        outOfBound = true;
        if(GetComponent<RotateRb>() is RotateRb rrb)
        {
            rrb.Spin(outSpin);
        }
    }

    public void StartRunning()
    {
        IsRunning = true;
        Debug.Log("he");
    }
}
