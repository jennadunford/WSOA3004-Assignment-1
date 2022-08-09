using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGen : MonoBehaviour
{
    public Tile[] tiles;

    public Tilemap tileMap;
    public Tilemap obstacleMap;
    public Tilemap dirSwapMap;

    private DirectionChangeManager dirMan;

    [SerializeField] int width = 9;
    [SerializeField] int maxStraigth = 30, minStright = 5;
    [SerializeField] int obsticleGap = 4;
    private int obsticleCount;

    public Vector3Int lastSpawnCoord;

    public bool notStart = false;

    public bool makeNew = false;
    public bool makeNew2 = false;

    int y = 0;
    //0: middleNormal
    //1: innerLane
    //2: outerLane
    //3: leftSide
    //4: rightSide
    //5: rightleft
    //6: rightSideHalf
    //7: leftSideHalf
    //8: obstacle
    // Start is called before the first frame update
    void Start()
    {
        dirMan = dirSwapMap.GetComponent<DirectionChangeManager>();

        bool start = true;
        CreateStraight(0, 0, Random.Range(minStright, maxStraigth), start);
        InvokeRepeating("endlessGeneration", 1, 5);
    }

    public void CreateStraight(int startx, int starty, int length, bool beginning)
    {
        obsticleCount = 0;
        if (beginning)
        {
            Vector3Int pos = new Vector3Int((startx - 1), (starty - 1), 0);
            addTile(5, pos);
            for (int x = 0; x < 9; x++)
            {
                Vector3Int pos2 = new Vector3Int((startx + x), (starty - 1), 0);
                addTile(4, pos2);
            }
            Vector3Int pos3 = new Vector3Int(startx + 9, starty - 1, 0);
            addTile(6, pos3);
            Vector3Int pos4 = new Vector3Int(startx - 1, starty, 0);
            addTile(3, pos4);
        }
        for (int x = 0; x < length - 1; x++)
        {
            Vector3Int pos = new Vector3Int((startx - 1), (starty + x + 1), 0);
            addTile(3, pos);
        }
        for (int j = 0; j < length; j++)
        {
            for (int i = 0; i <= width; i++)
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
                if (!beginning && j % obsticleGap == 0)
                {
                    //addObst(8, pos);
                }
            }
            
        }
        //OBSTACLE GENERATION FOR STRAIGHT 
       /* int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < length - 2; j++)
                    {
                        Vector3Int posObj = new Vector3Int(startx + 1 + i, starty + 2 + j, 0);
                        addObst(8, posObj);
                    }
                }
                break;
            case 1:
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < length - 2; j++)
                    {
                        Vector3Int posObj = new Vector3Int(startx + 4 + i, starty + 2 + j, 0);
                        addObst(8, posObj);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < length - 2; j++)
                    {
                        Vector3Int posObj = new Vector3Int(startx + 7 + i, starty + 2 + j, 0);
                        addObst(8, posObj);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < length - 2; j++)
                    {
                        Vector3Int posObj = new Vector3Int(startx + 1 + i, starty + 2 + j, 0);
                        addObst(8, posObj);
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < length - 2; j++)
                    {
                        Vector3Int posObj = new Vector3Int(startx + 7 + i, starty + 2 + j, 0);
                        addObst(8, posObj);
                    }
                }
                break;
        }*/
        makeNew = true;
    }

    public void createTurnRight(int startx, int starty)
    {
        for (int x = 0; x <= width; x++)
        {
            Vector3Int pos = new Vector3Int((startx - 1), (starty + x), 0);
            addTile(3, pos);
        }
        Vector3Int pos2 = new Vector3Int((startx - 1), (starty + 10), 0);
        addTile(7, pos2);

        dirMan.AddDirChange(pos2.x, pos2.y - (width/2 + 1), PlayerBounds.LaneDirection.Right);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 11; j++)
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
                else if (i == 9 && j == 1)
                {
                    addTile(2, pos);
                    lastSpawnCoord = pos;

                }
                else if (i == 3 && j <= 7)
                {
                    addTile(1, pos);
                }
                else if (j == 7 && i >= 3)
                {
                    addTile(1, pos);
                }
                else if (i == 6 && j <= 4)
                {
                    addTile(1, pos);
                }
                else if (j == 4 && i >= 6)
                {
                    addTile(1, pos);
                }
                else if (j == 0 && i == 9)
                {
                    addTile(2, pos);
                }
                else
                {
                    addTile(0, pos);
                }

                // Add corner postion swapper
                if(i == j)
                {
                   AddDirectionSwap(8, pos);
                }


            }
        }
        makeNew = false;
        makeNew2 = true;
    }
    public void createStraightRight(int startx, int starty, int length)
    {

        for (int x = 0; x < length; x++)
        {
            Vector3Int pos = new Vector3Int((startx + x + 1), (starty - 10), 0);
            addTile(4, pos);
            pos = new Vector3Int((startx + x + 1), (starty - 9), 0);
            addTile(2, pos);
            pos = new Vector3Int((startx + x + 1), (starty), 0);
            lastSpawnCoord = pos;
            addTile(2, pos);
            pos = new Vector3Int((startx + x + 1), (starty - 3), 0);
            addTile(1, pos);
            pos = new Vector3Int((startx + x + 1), (starty - 6), 0);
            addTile(1, pos);
            pos = new Vector3Int((startx + x + 1), (starty - 1), 0);
            addTile(0, pos);
            pos = new Vector3Int((startx + x + 1), (starty - 2), 0);
            addTile(0, pos);
            pos = new Vector3Int((startx + x + 1), (starty - 4), 0);
            addTile(0, pos);
            pos = new Vector3Int((startx + x + 1), (starty - 5), 0);
            addTile(0, pos);
            pos = new Vector3Int((startx + x + 1), (starty - 7), 0);
            addTile(0, pos);
            pos = new Vector3Int((startx + x + 1), (starty - 8), 0);
            addTile(0, pos);
        }

        //OBSTACLE GENERATION FOR RIGHT STRAIGHT (AFTER RIGHT TURNS)
        int rand = Random.Range(0, 4);


        switch (rand)
        {
            case 0:
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        Vector3Int posObj = new Vector3Int(startx + 1 + j, starty - 2 + i, 0);
                        addObst(8, posObj);
                    }
                }
                break;
            case 1:
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        Vector3Int posObj = new Vector3Int(startx + 2 + j, starty - 5 + i, 0);
                        addObst(8, posObj);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        Vector3Int posObj = new Vector3Int(startx + 2 + j, starty - 8 + i, 0);
                        addObst(8, posObj);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        Vector3Int posObj = new Vector3Int(startx + 2 + j, starty - 2 + i, 0);
                        addObst(8, posObj);
                    }

                }
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        Vector3Int posObj = new Vector3Int(startx + 2 + j, starty - 8 + i, 0);
                        addObst(8, posObj);
                    }
                }
                break;
        }



    }

    public void createTurnLeft(int startx, int starty)
    {
        for (int x = 0; x < 9; x++)
        {
            Vector3Int pos = new Vector3Int((startx + x + 1), (starty - 10), 0);
            addTile(4, pos);
        }
        Vector3Int pos2 = new Vector3Int((startx + 9), (starty - 10), 0);
        addTile(6, pos2);
        dirMan.AddDirChange(pos2.x - width / 2, pos2.y, PlayerBounds.LaneDirection.Left);
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector3Int pos3 = new Vector3Int(startx + i + 1, starty - j, 0);
                if ((i <= 2 && j == 3) || (i <= 5 && j == 6) || (i == 2 && j <= 3) || (i == 5 && j <= 6))
                {
                    addTile(1, pos3);
                }
                else if ((i == 8) || (j == 9))
                {
                    addTile(2, pos3);
                }
                else
                {
                    addTile(0, pos3);
                }

                if (i == j)
                {
                    AddDirectionSwap(8, pos3);
                }
            }
        }

    }





    public void addTile(int tileNum, Vector3Int position)
    {
        tileMap.SetTile(position, tiles[tileNum]);
    }

    public void addObst(int tileNum, Vector3Int position)
    {
        obstacleMap.SetTile(position, tiles[tileNum]);
    }

    public void AddDirectionSwap(int tileNum, Vector3Int position)
    {
        dirSwapMap.SetTile(position, tiles[tileNum]);
    }
    public void endlessGeneration()
    {
        if (makeNew)
        {
            createTurnRight(lastSpawnCoord.x, lastSpawnCoord.y);

        }
        if (makeNew2)
        {
            createStraightRight(lastSpawnCoord.x, lastSpawnCoord.y, Random.Range(3, 20));
            createTurnLeft(lastSpawnCoord.x, lastSpawnCoord.y);
            CreateStraight(lastSpawnCoord.x, lastSpawnCoord.y, Random.Range(3, 20), false);
            makeNew2 = false;
        }
    }
}
