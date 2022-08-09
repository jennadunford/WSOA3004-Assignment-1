using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class spawnSharks : MonoBehaviour
{
    //Script for generating sharks and the colliders for the sharks
    public Tilemap sharkMap; //Tile map holding the sharks
    public Tilemap sharkCollider; //Tile map holding colliders for the sharks
    public Tile[] sharkTiles; //Tile array holding shark and collider tiles
    // Start is called before the first frame update
    void Start()
    {
        int x = 3; //x position of shark 1
        int y = 3; //y position of shark 1
        int x2 = 5; //x position of shark 2
        int y2 = -3; //y position of shark 2
        Vector3Int pos = new Vector3Int(x, y, 0); //position of shark 1
        sharkMap.SetTile(pos, sharkTiles[0]); //placing shark 1
        for(int i = 0; i < 2; i++)//For loop creating square of collider tiles under shark
        {
            for(int j = 0; j < 2; j++)
            {
                pos = new Vector3Int(x + i-2, y + j -2,0); //position of each shark collider tile
                sharkCollider.SetTile(pos, sharkTiles[2]);
            }
        }
        pos = new Vector3Int(x2, y2, 0 ); //position of shark 2
        sharkMap.SetTile(pos, sharkTiles[1]); //Placing shark 2
        for (int i = 0; i < 2; i++) //For loop creating square of tiles under shark 2
        {
            for (int j = 0; j < 2; j++) 
            {
                pos = new Vector3Int(x2 + i - 2, y2 + j - 2, 0); //positions of shark 2 tiles
                sharkCollider.SetTile(pos, sharkTiles[2]);
            }
        }
    }
}
