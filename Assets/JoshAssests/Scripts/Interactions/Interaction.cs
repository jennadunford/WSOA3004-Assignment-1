using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class used as an acestore for mutiple interations such as direction changes and pickups
public abstract class Interaction : MonoBehaviour
{
    public abstract void Interact(PlayerBounds playerBounds);
}
