using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TempTileMaker : MonoBehaviour
{
    [SerializeField] private Tilemap floorMap; // This tile map is used as a trigger for movment
    [SerializeField] private Tilemap fxMap; // This tile map conitnes 'side tiles' and other tiles that don't funtion as a path for the player

    [SerializeField] int rows, cols, rStart, cStart;
    [SerializeField] Tile floor;
    [SerializeField] Tile side;
    void Start()
    {
        MakeMap();
    }

    private void MakeMap()
    {
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                floorMap.SetTile(new Vector3Int(i + rStart, j + cStart, 0), floor);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
