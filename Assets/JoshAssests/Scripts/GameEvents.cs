using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameEvents : MonoBehaviour
{
    public UnityEvent GameStart;
    public UnityEvent<string> GameEnd;

   

    [TextArea] public string outOfBoundsMsg;
    [TextArea] public string sharkMsg;

    // public methods
    public void StartGame()
    {
        GameStart.Invoke();
    }

    public void OutOfBounds()
    {
        GameEnd.Invoke(outOfBoundsMsg);
    }

    public void HitShark()
    {
        GameEnd.Invoke(sharkMsg);
    }
}
