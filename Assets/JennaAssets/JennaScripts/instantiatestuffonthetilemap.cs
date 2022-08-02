using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class instantiatestuffonthetilemap : MonoBehaviour
{
    public Grid gameGrid;
    public Tilemap gameTiles;
    public Tile[] paletteTiles;

    // Start is called before the first frame update
    void Start()
    {
        Vector3Int position;

        int xpos = 0;
        int ypos = 0;
        int rand = Random.Range(5, 11);
        int rand2 = Random.Range(5, 11);
            for (int i = 0; i <= rand; i++)
            {
         
                for (int j = 0; j <= rand2; j++)
                {
                    position = new Vector3Int(xpos + i, ypos + j, 0);
                    addTile(Random.Range(0, 2), position);
                }
            }
       
        
    }
    void addTile(int tileNum, Vector3Int position)
    {
        gameTiles.SetTile(position, paletteTiles[tileNum]);
    }
}

