using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedRunnerCtrl : MonoBehaviour
{
    [SerializeField] PlayerBounds bounds;
    [SerializeField] private float forwardSpeed;
    [SerializeField] string shiftAxis = "Horizontal";
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bounds.SetPlayer(rb);
        StartRunning();
    }

    private void Update()
    {
        rb.AddForce(Input.GetAxis(shiftAxis) * bounds.Axese.colVect);
        rb.velocity = bounds.Axese.ColComponentVect(rb.velocity) + bounds.Axese.rowVect * forwardSpeed;

        Debug.Log(bounds.Axese.RowComponent(rb.velocity));
    }

    public void StartRunning()
    {
        rb.velocity = bounds.Axese.rowVect * forwardSpeed;
    }
}
