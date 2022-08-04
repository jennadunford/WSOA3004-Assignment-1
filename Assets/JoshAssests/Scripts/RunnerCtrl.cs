using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCtrl : MonoBehaviour
{
    const string inputShiftAxis = "Horizontal";
    [SerializeField] private Grid grid;
    [SerializeField] private LaneDirection laneDirection;

    [SerializeField] private float laneSpeed = 1f, shiftSpeed = 1f;
    [SerializeField] bool normalized;
    private bool isShifting = false;

    private Rigidbody2D rb;


    public Vector3 LaneDir { get; protected set; }
    public Vector3 ShiftDir { get; protected set; }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDirection();

    }

    void Update()
    {
        Move();
    }

    // Setup methods
    public void SetDirection()
    {
        Vector3 origin = grid.CellToWorld(new Vector3Int(0, 0, 0));
        Vector3 laneRef, shiftRef;


        if (laneDirection == LaneDirection.Right)
        {
            laneRef = grid.CellToWorld(new Vector3Int(1, 0, 0));
            shiftRef = grid.CellToWorld(new Vector3Int(0, 1, 0));
        }
        else
        {
            laneRef = grid.CellToWorld(new Vector3Int(0, 1, 0));
            shiftRef = grid.CellToWorld(new Vector3Int(1, 0, 0));
        }

        LaneDir = (laneRef - origin).normalized;
        ShiftDir = (shiftRef - origin).normalized;
    }
    public void SetDirection(LaneDirection dir)
    {
        if (laneDirection != dir)
        {
            laneDirection = dir;
            SetDirection();
        }

    }

    // Allignment
    private void AllignPath()
    {
        Vector3 gridCord = grid.WorldToCell(transform.position);
        Debug.Log(gridCord);
    }

    // Co-or finding 
    /* private float GetLaneOffset()
     {

     }
     private float GetShiftOffset()
     {

     }

     private Vector3 shiftVect()
     {

     }*/
    private Vector3 GetCellCentre()
    {
        return grid.CellToWorld(grid.WorldToCell(transform.position));
    }
    // Movement
    private void Move()
    {
        rb.velocity = GetForwardMove() + GetSideMove();
    }
    private Vector3 GetForwardMove()
    {
        return laneSpeed * LaneDir;
    }
    private Vector3 GetSideMove()
    {
        return Input.GetAxis(inputShiftAxis) * shiftSpeed * ShiftDir;
    }

    [System.Serializable]
    public enum LaneDirection
    {
        Right,
        Left
    }
}
