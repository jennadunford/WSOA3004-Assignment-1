using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedRunnerCtrl : MonoBehaviour
{
    [SerializeField] PlayerBounds bounds;
    [SerializeField] private float forwardSpeed = 1f, shiftForce = 1f, maxShiftSpeed = 1000f, outDrag = 1f, maxOutSpeed, outSpin = 10f;
    [SerializeField] string shiftAxis = "Horizontal";

    private bool ready = true;
    public bool IsRunning { get; private set; } = false;
    private bool outOfBound = false;
    private Rigidbody2D rb;
    private CollisonId colId;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(!(colId = GetComponent<CollisonId>()))
        {
            Debug.LogWarning("No collion ID checker attached to player obejct " + gameObject);
        }

        colId.OnObsticleHit.AddListener(OnObsticleHit);

        bounds.SetPlayer(rb);
        bounds.OnOutOfBounds.AddListener(OutOfBounds);
        bounds.OnDirectionChange.AddListener(OnDirectionChange);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { bounds.SwapDirection(); }
        if (IsRunning)
        {
            rb.AddForce(Input.GetAxis(shiftAxis) * bounds.GetInputCorrection() * shiftForce * bounds.Axese.rowVect);
            Vector2 shiftComp = bounds.Axese.RowComponentVect(rb.velocity);
            if(shiftComp.magnitude >= maxShiftSpeed)
            {
                shiftComp = maxShiftSpeed * shiftComp.normalized;
            }
            Vector2 forwardComp = bounds.Axese.colVect * forwardSpeed;
            rb.velocity = shiftComp + forwardComp;
            bounds.CheckBounds();
        }
        else if (outOfBound && bounds.Axese.ColComponent(rb.velocity) > -maxOutSpeed)
        {
            // Gradually deccelerate 
            rb.AddForce(-outDrag * bounds.Axese.colVect);
        }
        else if(ready)
        {
            if (Input.anyKeyDown)
            {
                ready = false;
                StartRunning();
            }
        }
    }

    // game managment events
    private void OutOfBounds()
    {
        IsRunning = false;
        outOfBound = true;
        if(GetComponent<RotateRb>() is RotateRb rrb)
        {
            rrb.Spin(outSpin);
        }
    }
    private void OnObsticleHit()
    {
        IsRunning = false;
        if (GetComponent<RotateRb>() is RotateRb rrb)
        {
            rrb.Spin(outSpin);
        }
    }

    private void OnDirectionChange(nonOrthoAxis axis)
    {
        rb.velocity = new Vector2(0, 0);
    }

    public void StartRunning()
    {
        IsRunning = true;
    }
}
