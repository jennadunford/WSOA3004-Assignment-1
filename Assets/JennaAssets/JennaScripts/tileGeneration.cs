using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileGeneration : MonoBehaviour
{
    public Tile[] tiles;

    public Tilemap tileMap;

    public Vector3Int lastSpawnCoord;

    public bool notStart = false;

    public bool makeNew = false;

    int y = 0;
    //0: middleNormal
    //1: innerLane
    //2: outerLane
    //3: leftSide
    //4: rightSide
    //5: rightleft
    //6: rightSideHalf
    //7: leftSideHalf
    // Start is called before the first frame update
    void Start()
    {
        bool start = true;
        createStraight(0, 0,5, start);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (makeNew)
        {
            createTurnRight(lastSpawnCoord.x, lastSpawnCoord.y);
          
        }
       
        
    }

    public void createStraight(int startx, int starty, int length, bool beginning)
    {
       
        if(beginning)
        {
            Vector3Int pos = new Vector3Int((startx - 1), (starty -1), 0);
            addTile(5, pos);
            for(int x=0; x < 9; x++)
            {
                Vector3Int pos2 = new Vector3Int((startx + x), (starty - 1), 0);
                addTile(4, pos2);
            }
            Vector3Int pos3 = new Vector3Int(startx + 9, starty - 1, 0);
            addTile(6, pos3);
        }
        for(int x=0; x < length; x++)
        {
            Vector3Int pos = new Vector3Int((startx - 1), (starty+ x), 0);
            addTile(3, pos);
        }
        for(int i = 0; i<10; i++)
        {
            for(int j = 0; j < length; j++)
            {
                Vector3Int pos = new Vector3Int((startx + i), (starty + j), 0);
                switch (i)
                {
                    case 0:
                        addTile(2, pos);
                        lastSpawnCoord = pos;
                        break;
                    case 3:
                        addTile(1, pos);
                        break;
                    case 6:
                        addTile(1, pos);
                        break;
                    case 9:
                        addTile(2, pos);
                        break;
                    default:
                        addTile(0, pos);
                        break;
                }
            }

        }

        makeNew = true;
    }

    public void createTurnRight(int startx, int starty)
    {
        for (int x = 0; x < 10; x++)
        {
            Vector3Int pos = new Vector3Int((startx - 1), (starty + x), 0);
            addTile(3, pos);
        }
        Vector3Int pos2 = new Vector3Int((startx - 1), (starty + 10), 0);
        addTile(7, pos2);

        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j<11; j++)
            {
                Vector3Int pos = new Vector3Int((startx + i), (starty + j), 0);
                if (i == 0)
                {
                    addTile(2, pos);
                   
                }
                else if (j == 10)
                {
                    addTile(2, pos);
                    lastSpawnCoord = pos;
                }
                else if(i == 9 && j == 1)
                {
                    addTile(2, pos);
                    lastSpawnCoord = pos;

                }
                else if(i ==3 && j <= 7)
                {
                    addTile(1, pos);
                }else if(j == 7 && i >= 3)
                {
                    addTile(1, pos);
                }else if (i == 6 && j <=4)
                {
                    addTile(1, pos);
                }else if(j ==4 && i >= 6)
                {
                    addTile(1, pos);
                }
                else if(j==0 && i == 9)
                {
                    addTile(2, pos);
                }
                else
                {
                    addTile(0, pos);
                }


            }
        }
        makeNew = false;
    }

    public void createStraightRight(int startx, int starty, int length)
    {

        for (int x = 0; x < length; x++)
        {
            Vector3Int pos = new Vector3Int((startx - x), (starty + 1), 0);
            addTile(4, pos);
        }
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Vector3Int pos = new Vector3Int((startx + i), (starty + j), 0);
                switch (i)
                {
                    case 0:
                        addTile(2, pos);
                        lastSpawnCoord = pos;
                        break;
                    case 3:
                        addTile(1, pos);
                        break;
                    case 6:
                        addTile(1, pos);
                        break;
                    case 9:
                        addTile(2, pos);
                        break;
                    default:
                        addTile(0, pos);
                        break;
                }
            }

        }

      
    }


    public void createTurnLeft(int startx, int starty, int length)
    {

    }

    public void createStraightLeft(int startx, int starty, int length)
    {

    }


    public void addTile(int tileNum, Vector3Int position)
    {
        tileMap.SetTile(position, tiles[tileNum]);
    }
}