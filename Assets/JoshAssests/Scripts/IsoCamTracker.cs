using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows camaer to follow player along a single isometric axis, rather than in full x-y plane
// This makes movment feel smoother and more natural in the isometic space
public class IsoCamTracker : MonoBehaviour
{
    [SerializeField] private PlayerBounds bounds;
    void Update()
    {
        if (bounds.PlayerRb)
        {
            // move along column only
            transform.position = (Vector3)bounds.StartPos + (Vector3)bounds.Axese.ColComponentVect(bounds.Displacment) + new Vector3(0, 0, transform.position.z);
        }
    }
}
