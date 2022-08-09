using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script flips the turtle when it changes direction so that the isometric sprite is never 'upside dow'

public class FlipTurtle : MonoBehaviour
{
    private PlayerBounds bounds;
    private SpriteRenderer sr;

    private void Awake()
    {
        bounds = GetComponent<PlayerBounds>();
        sr = GetComponent<SpriteRenderer>();
        bounds.OnDirectionChange.AddListener(OnBoundsDirectionChange);
    }

    private void OnBoundsDirectionChange(nonOrthoAxis axis)
    {
        CheckFlip();
    }

    private void CheckFlip()
    {
        sr.flipY = bounds.GetDirection() == PlayerBounds.LaneDirection.Left;
    }
}
