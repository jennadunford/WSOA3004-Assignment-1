using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Used for isometric vector operations
// Iso vectros are split into two compeoents
// Row runs up + right at 60 deg
// Col runs up + left at 150 deg
public class IsoVect
{
    private readonly float isoAnlge;

    // Axis unit vectors 
    public readonly Vector2 rowVect;
    public readonly Vector2 colVect;
    public IsoVect(Vector2 _rowVect, Vector2 _colVect)
    {
        rowVect = _rowVect.normalized;
        colVect = _colVect.normalized;

        Vector2 refVect = new Vector2(1, 0);

        isoAnlge = 2f * Vector2.Angle(rowVect, refVect);
    }


    // === Break a vector into its componentes ===

    // Returns the maganaturde of the row component of vector 'vect'
    public float RowComponent(Vector2 vect)
    {
        float angle = Vector2.SignedAngle(vect, colVect); // note direction of angle
        Debug.Log("Col angle is " + angle);

        // use sin rule to find length of other side
        return Mathf.Sin(angle) * (vect.magnitude / Mathf.Sin(isoAnlge));
    }
    // Returns the maganaturde of the col[umn] component of vector 'vect'
    public float ColComponent(Vector2 vect)
    {
        float angle = Vector2.SignedAngle(rowVect, vect); // note direction of angle
        Debug.Log("row angle is " + angle);

        // use sin rule to find length of other side
        return Mathf.Sin(angle) * (vect.magnitude / Mathf.Sin(isoAnlge));
    }
    // Return a new vector2 of (isoRow, isoColumn)
    public Vector2 IsoComponents(Vector2 vect)
    {
        return new Vector2(RowComponent(vect), ColComponent(vect));
    }
    // return the row component vector
    public Vector2 RowComponentVect(Vector2 vect)
    {
        return RowComponent(vect) * rowVect;
    }
    public Vector2 ColComponentVect(Vector2 vect)
    {
        return ColComponent(vect) * colVect;
    }
}
