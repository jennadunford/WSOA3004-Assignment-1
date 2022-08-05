using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedRunnerCtrl : MonoBehaviour
{
    [SerializeField] PlayerBounds bounds;
    [SerializeField] private float forwardSpeed= 1f, shiftForce = 1f;
    [SerializeField] string shiftAxis = "Horizontal";
    private int inputCorrection = 1;

    public bool IsRunning { get; private set; } = false;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bounds.SetPlayer(rb);
        bounds.OnOutOfBounds.AddListener(GG);
        StartRunning();
    }

    private void Update()
    {
        if (IsRunning)
        {
            rb.AddForce(Input.GetAxis(shiftAxis) * bounds.GetInputCorrection() * shiftForce * bounds.Axese.rowVect);
            rb.velocity = bounds.Axese.RowComponentVect(rb.velocity) + bounds.Axese.colVect * forwardSpeed;
        }
    }

    private void GG()
    {
        Debug.Log("outtutut");
    }

    public void StartRunning()
    {
        IsRunning = true;
    }
}
