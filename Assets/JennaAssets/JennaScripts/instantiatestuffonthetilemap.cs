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
        //position = new Vector3Int(0, 0, 0);
        //gameTiles.SetTile(position,paletteTiles[0]);
        //Vector3Int position;
        //position = new Vector3Int(0, 0, 0);
        //addTile(0, position);


        for (int x = 0; x < Random.Range(5,10); x++)
        {
            position = new Vector3Int(x+1, x + 1, x);
            addTile(Random.Range(0, 2), position);
            position = new Vector3Int(x + 4, x + 1, x);
            addTile(Random.Range(0, 2), position);
            position = new Vector3Int(x, x+1, x);
            addTile(Random.Range(0, 2), position);
            position = new Vector3Int(-x, -x, -x);
            addTile(Random.Range(0, 2), position);


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addTile(int tileNum, Vector3Int position)
    {
        gameTiles.SetTile(position, paletteTiles[tileNum]);
    }
}
