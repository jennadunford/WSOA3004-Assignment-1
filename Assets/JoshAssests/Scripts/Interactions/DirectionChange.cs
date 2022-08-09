using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChange : Interaction
{
    [SerializeField] private bool isSet = false;
    [SerializeField] private int startX, startY;

    // Must be called when direction change is added to set the bounds for the new path
    public void Set(int pathStartX, int pathStartY)
    {
        startX = pathStartX;
        startY = pathStartY;
        isSet = true;
    }

    public override void Interact(PlayerBounds playerBounds)
    {
        if (isSet)
        {
            playerBounds.SwapDirection(startX, startY);
        }
        else
        {
            throw new System.Exception("Attepting to interact with an unset Direction changer " + gameObject + ". Direction chnagers must be set when instasiated");
        }
    }
}
