using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IsoMoveCtrl : MonoBehaviour
{
    [SerializeField] protected Grid grid;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed, xBaseSpeed, yBaseSpeed, normalizedSpeed;
    [SerializeField] bool moveX = true, moveY = true, normalizeFinalMove;


    [SerializeField] private string xAxis = "Horizontal", yAxis = "Vertical";

    public Vector3 IsoX { get; private set; }
    public Vector3 IsoY { get; private set; }
    

    public Vector3Int GridPos { get; protected set; }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetIsoVectors();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Setup methods
    private void SetIsoVectors()
    {
        Vector3 orign = grid.CellToWorld(new Vector3Int(0, 0, 0));
        Vector3 xRef = grid.CellToWorld(new Vector3Int(1, 0, 0));
        Vector3 yRef = grid.CellToWorld(new Vector3Int(0, 1, 0));

        IsoX = (xRef - orign).normalized;
        IsoY = (yRef - orign).normalized;
    }

    // Control methods
    private void Move()
    {
        Vector3 velocity = GetXMove() + GetYMove();
        if (normalizeFinalMove)
        {
            velocity = normalizedSpeed * velocity.normalized;
        }
        rb.velocity = velocity;
    }

    private Vector3 GetXMove()
    {
        float input = 0f;
        if (moveX)
        {
            input = Input.GetAxis(xAxis) * moveSpeed;
        }
        return (input + xBaseSpeed) * IsoX;
    }
    private Vector3 GetYMove()
    {
        float input = 0f;
        if (moveY)
        {
            input = Input.GetAxis(yAxis) * moveSpeed;
        }
        return (input + yBaseSpeed) * IsoY;
    }

}
