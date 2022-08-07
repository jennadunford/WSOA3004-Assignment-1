using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Responsable for defining the axis and bundies that the player moves on
// These boundies can be defined by and given unity grid, isometric or otherwise
public class PlayerBounds : MonoBehaviour
{
    [SerializeField] private Grid grid;

    [SerializeField] int startX, startY, lowLane = 1, highLane = 1;
    [SerializeField] float tollerance = 0f; // How far out of bounds can player move before event is triggered
    private float lowBound, highBound;

    [SerializeField] private LaneDirection laneDirection;

    public Rigidbody2D PlayerRb { get; private set; }
    public Vector2 StartPos { get; private set; }
    public Vector2 Displacment { get; private set; }
    private Vector2 row, col;
    public nonOrthoAxis Axese { get; set; }


    // Events
    public UnityEvent OnOutOfBounds; // called when player goes out of bounds
    public UnityEvent<nonOrthoAxis> OnDirectionChange; // called when player changes direction (swaps foward axis)

    private void Awake()
    {
        SetDirection(laneDirection);
    }
    private void FixedUpdate()
    {

    }
    // === Player setup ===
    public void SetPlayer(Rigidbody2D playerRb)
    {
        PlayerRb = playerRb;
        AllignPlayer();
    }


    // === Axis setup ===
    public void SetDirection(LaneDirection dir)
    {
        laneDirection = dir;
        if (dir == LaneDirection.Right)
        {
            col = GetXVect();
            row = GetYVect();
        }
        else
        {
            col = GetYVect();
            row = GetXVect();
        }
        Axese = new nonOrthoAxis(row, col);

        OnDirectionChange?.Invoke(Axese);
    }

    public void SwapDirection(int laneX, int laneY)
    {
        startX = laneX;
        startY = laneY;
        if (laneDirection == LaneDirection.Right)
        {
            SetDirection(LaneDirection.Left);
        }
        else
        {
            SetDirection(LaneDirection.Right);
        }


        // Redfine lane bounds
        StartPos = CellToTile(startX, startY);
        SetBounds(StartPos);

    }

    // returns and isometric 'unit' vector for x grid rows
    public Vector3 GetXVect()
    {

        return grid.CellToWorld(new Vector3Int(1, 0, 0)) - grid.CellToWorld(new Vector3Int(0, 0, 0));
    }
    // retruns and isometric 'unit' vector for y grid rows
    public Vector3 GetYVect()
    {
        return grid.CellToWorld(new Vector3Int(0, 1, 0)) - grid.CellToWorld(new Vector3Int(0, 0, 0));
    }

    // === player allignment ===
    private void AllignPlayer()
    {
        StartPos = CellToTile(startX, startY);
        PlayerRb.MovePosition(StartPos);

        SetBounds(StartPos);
    }


    // === bounds checking === //
    private void SetBounds(Vector2 start)
    {
        // assume that rows and colums have the same size
        Vector2 lowDis = -1 * (lowLane + 0.5f) * grid.cellSize.y * Axese.rowVect;
        Vector2 highDis = (highLane + 0.5f) * grid.cellSize.y * Axese.rowVect;

        lowBound = Axese.RowComponent(lowDis);
        highBound = Axese.RowComponent(highDis);
    }

    public void CheckBounds()
    {
        Displacment = PlayerRb.position - StartPos;
        float deltaRow = Axese.RowComponent(Displacment);

        if(deltaRow < lowBound - tollerance|| deltaRow > highBound + tollerance)
        {
            OnOutOfBounds?.Invoke();
        }
    }

    // === Getters and setters ===
    public Grid GetGrid() { return grid; }
    public LaneDirection GetDirection() { return laneDirection; }

    // returns corrected input to ensure axes act intutivly
    public int GetInputCorrection()
    {
        if (laneDirection == LaneDirection.Right) { return -1; }
        return 1;
    }

    // Useful classes, enums and structs

    [System.Serializable]
    public enum LaneDirection
    {
        Right,
        Left
    }


    // === static Tile postion mananment ===
    private Vector2 CellToTile(int x, int y)
    {
        return CellToTile(new Vector2Int(x, y));
    }
    private Vector2 CellToTile(Vector2Int cellCords)
    {
        return GetTileCoords(grid.CellToWorld((Vector3Int)cellCords));
    }
    // Find the co-ods of a tile hat corresposnds to a row
    private Vector3 GetTileCoords(Vector2 CellCoords)
    {
        // Find centre of tile in the positvie direction along rows and colums
        // Assume no cell gap
        Vector2 rowShit = 0.5f * row;
        Vector2 colShift = 0.5f * col;

        return CellCoords + rowShit + colShift;
    }

}
