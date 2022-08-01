using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IsoMoveCtrl : MonoBehaviour
{
    [SerializeField] protected Grid grid;
    private Rigidbody2D rb;
    [SerializeField] private float speed;

    public Vector3 IsoX { get; private set; }
    public Vector3 IsoZ { get; private set; }
    

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


        transform.position += speed * Time.deltaTime * (Input.GetAxis("Vertical") * IsoZ + Input.GetAxis("Horizontal") * IsoX);

    }

    // Setup methods
    private void SetIsoVectors()
    {
        Vector3 orign = grid.CellToWorld(new Vector3Int(0, 0, 0));
        Vector3 xRef = grid.CellToWorld(new Vector3Int(1, 0, 0));
        Vector3 zRef = grid.CellToWorld(new Vector3Int(0, 1, 0));

        IsoX = (xRef - orign).normalized;
        IsoZ = (zRef - orign).normalized;
    }

    // Control methods
    private void Move() // called in fixed ud
    {

    }

}
