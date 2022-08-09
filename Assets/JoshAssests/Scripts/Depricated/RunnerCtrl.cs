using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCtrl : MonoBehaviour
{
    const string inputShiftAxis = "Horizontal";
    [SerializeField] private Grid grid;

    [SerializeField] int startRow, startColumn;
    [SerializeField] int sideMin, sideMax;
    public Vector2 DeltaPos { get; private set; }
    private Vector2 startPos;

    [SerializeField] private LaneDirection laneDirection;
    [SerializeField] private bool invertLeft = false, invertRight = true;

    [SerializeField] private float laneSpeed = 1f, shiftSpeed = 1f, shitfSpeedMin = 1f, shiftSpeedMax = 5f, shiftAcceleration;
    private float currShiftSpeed;
    private bool isShifting = true; // assume that tuttle is originally moving to help setup

    private Rigidbody2D rb;


    public Vector3 LaneDir { get; protected set; }
    public Vector3 ShiftDir { get; protected set; }



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetDirection();

        // Move rb to its start postion
        startPos = grid.CellToWorld(new Vector3Int(startRow, startColumn, 0));
        startPos = GetTileCoords(startPos);
        rb.MovePosition(startPos);
    }

    void FixedUpdate()
    {
        DeltaPos = rb.position - startPos;
        Move();
    }

    // Setup methods
    // -> Vecotr finding
    public void SetDirection()
    {
        if (laneDirection == LaneDirection.Right)
        {
            LaneDir = GetRowVect();
            ShiftDir = GetColVect();
        }
        else
        {
            LaneDir = GetColVect();
            ShiftDir = GetRowVect();
        }
    }

    // returns and isometric 'unit' vector for rows
    public Vector3 GetRowVect()
    {
        return grid.CellToWorld(new Vector3Int(1, 0, 0)) - grid.CellToWorld(new Vector3Int(0, 0, 0));
    }
    // retruns and isometric 'unit' vector for columens
    public Vector3 GetColVect()
    {
        return grid.CellToWorld(new Vector3Int(0, 1, 0)) - grid.CellToWorld(new Vector3Int(0, 0, 0));
    }
    public void SetDirection(LaneDirection dir)
    {
        if (laneDirection != dir)
        {
            laneDirection = dir;
            SetDirection();
        }

    }
    // Movement
    private void Move()
    {
        rb.velocity = GetForwardMove() + GetSideMove();
    }
    private Vector3 GetForwardMove()
    {
        return GetForwardSpeed() * LaneDir;
    }
    private Vector3 GetSideMove()
    {
        int invert = 1;
        if(invertLeft && laneDirection == LaneDirection.Left || invertRight && laneDirection == LaneDirection.Right)
        {
            invert = -1;
        }

        bool shiftInput = Input.GetAxis(inputShiftAxis) != 0;
        if (isShifting && !shiftInput)
        {
            // reset shift speed
            currShiftSpeed = shiftSpeed;
        }
        isShifting = shiftInput;
        if (!isShifting)
        {
            return new Vector3(0, 0, 0);
        }

        // Player is still moving
        return Input.GetAxis(inputShiftAxis) * GetShiftSpeed() * invert * ShiftDir;
    }

    private float GetForwardSpeed()
    {
        return laneSpeed;
    }
    private float GetShiftSpeed()
    {
        if(shiftAcceleration > 0)
        {
            // ensure max speed is not exceeded
            if(currShiftSpeed < shiftSpeedMax)
            {
                currShiftSpeed += Time.fixedDeltaTime * shiftAcceleration;
            }
            else if(shiftSpeed > shiftSpeedMax)
            {
                currShiftSpeed = shiftSpeedMax;
            }
        }
        else if(shiftAcceleration < 0)
        {
            // Ensure that speed is never slower than min
            if(currShiftSpeed > shitfSpeedMin)
            {
                currShiftSpeed += currShiftSpeed += Time.fixedDeltaTime * shiftAcceleration;
            }
            else if (currShiftSpeed < shitfSpeedMin)
            {
                currShiftSpeed = shitfSpeedMin;
            }
        }

        return currShiftSpeed;
    }

    // === Tile postion mananment ===
    // Find the co-ods of a tile hat corresposnds to a row
    private Vector3 GetTileCoords(Vector3 CellCoords)
    {
        // Find centre of tile in the positvie direction along rows and colums
        // Assume no cell gap
        Vector3 rowShit = 0.5f * GetRowVect();
        Vector3 colShift = 0.5f * GetColVect();

        return CellCoords + rowShit + colShift;
    }




    [System.Serializable]
    public enum LaneDirection
    {
        Right,
        Left
    }
}
