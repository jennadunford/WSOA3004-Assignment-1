using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChangeManager : DirectionChange
{
    private readonly Queue<DirectionSwap> directionChanges = new Queue<DirectionSwap>();
    [SerializeField] private float cooldown = 1.5f;
    private float cd;

    private void Update()
    {
        if(cd < cooldown)
        {
            cd += Time.deltaTime;
        }
    }

    private struct DirectionSwap
    {
        public DirectionSwap(int _x, int _y, PlayerBounds.LaneDirection _dir) { x = _x; y = _y; dir = _dir; }
        public int x, y;
        public PlayerBounds.LaneDirection dir;
    }


    // === Plublic managment methods ===
    public void AddDirChange(int _x, int _y, PlayerBounds.LaneDirection dir)
    {
        directionChanges.Enqueue(new DirectionSwap(_x, _y, dir));
    }


    // === Overide methods ===
    public override void Interact(PlayerBounds playerBounds)
    {
        if(directionChanges.Count > 0 && cd >= cooldown)
        {
            DirectionSwap currChange = directionChanges.Peek();
            if (currChange.dir != playerBounds.GetDirection())
            {
                directionChanges.Dequeue();
                Set(currChange.x, currChange.y);
                base.Interact(playerBounds);
            }
        }

        // reset cooldown
        cd = 0;
    }

}
