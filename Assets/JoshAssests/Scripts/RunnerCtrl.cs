using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCtrl : MonoBehaviour
{
    const string inputShiftAxis = "Horizontal";
    [SerializeField] private Grid grid;
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

    }

    void FixedUpdate()
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
    [System.Serializable]
    public enum LaneDirection
    {
        Right,
        Left
    }
}
