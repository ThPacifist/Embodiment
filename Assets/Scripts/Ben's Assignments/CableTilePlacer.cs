using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(LineRenderer))]
public class CableTilePlacer : MonoBehaviour
{
    public enum Pipe
    {
        Hori,
        Vert,
        LU,
        LD,
        RU,
        RD
    }

    [SerializeField]
    LineRenderer line;
    Vector3Int currentPos;
    [SerializeField]
    GameObject startPos;
    [SerializeField]
    GameObject endPos;
    [Tooltip("Toggle this to make draw through tiles.")]
    [SerializeField]
    bool ignoreTilemap;
    [SerializeField]
    Tilemap tileMap;
    [SerializeField]
    Material WireRGBMat;

    [Header("List of Tiles")]
    public TileBase HorizontalCable;
    public TileBase VerticalCable;
    public TileBase leftUp;
    public TileBase leftDown;
    public TileBase rightUp;
    public TileBase rightDown;

    Vector3Int startPosR;
    Vector3Int endPosR;
    //Serves as the next position of the loop
    Vector3Int nextPos;
    List<Vector3Int> Positions = new List<Vector3Int>();
    List<TileBase> tileList = new List<TileBase>();
    int count = 0;

    public Dictionary<Pipe, TileBase> PipeDB;

    [ContextMenu("Initialize Tile Database")]
    void InitializeDB()
    {
        PipeDB = new Dictionary<Pipe, TileBase>();

        PipeDB.Add(Pipe.Hori, HorizontalCable);
        PipeDB.Add(Pipe.Vert, VerticalCable);
        PipeDB.Add(Pipe.LU, leftUp);
        PipeDB.Add(Pipe.LD, leftDown);
        PipeDB.Add(Pipe.RU, rightUp);
        PipeDB.Add(Pipe.RD, rightDown);

        Dictionary<Pipe, TileBase>.ValueCollection values = PipeDB.Values;
        foreach(TileBase t in values)
        {
            Debug.Log(t.name + " has been added to the dictionary");
        }
    }

    public void GenerateCable()
    {
        Positions.Clear();
        tileList.Clear();

        if (PipeDB == null)
            InitializeDB();

        if(tileMap == null)
        {
            ignoreTilemap = true;
            Debug.LogWarning("Tilemap has not been set. Will ignore tilemap by default.");
        }

        //Initializing TileMap
        GameObject CableMap = new GameObject("Cable Map");
        CableMap.transform.parent = this.transform.parent;
        Tilemap cableTileMap = CableMap.AddComponent<Tilemap>();
        TilemapRenderer tileRenderer = CableMap.AddComponent<TilemapRenderer>();
        tileRenderer.sortingOrder = 5;
        tileRenderer.material = WireRGBMat;
        TileMapColorChanger TMColorChanger = CableMap.AddComponent<TileMapColorChanger>();
        TMColorChanger.tileMapRenderer = tileRenderer;

        //Converts the positions of the game objects to be integer vectors
        startPosR = Vector3Int.FloorToInt(startPos.transform.position);
        startPosR.z = 0;
        endPosR = Vector3Int.FloorToInt(endPos.transform.position);
        endPosR.z = 0;
        Debug.Log("Start Pos is " + startPosR + ". End pos is " + endPosR + ".");

        //Initializes starting position
        currentPos = startPosR;
        nextPos = currentPos;
        Positions.Add(currentPos);
        tileList.Add(PipeDB[Pipe.Hori]);
        count = 1;
        int loops = 0;

        while (currentPos != endPosR && loops < 5)
        {
            //Updating the x position
            while (currentPos.x != endPosR.x)
            {
                //This is for when I am moving right instead of left
                int adjustment = 0;
                if (currentPos.x > endPosR.x)
                    adjustment = 2;

                //Sets the default tile
                TileBase nextTile = PipeDB[Pipe.Hori];
                nextPos.x += MoveTowardsInt(currentPos.x, endPosR.x);
                if (tileMap.GetTile(nextPos) && !ignoreTilemap)
                {
                    nextPos = currentPos;
                    //If there is not a tile above, move current pos up
                    if (!tileMap.GetTile(nextPos + Vector3Int.up))
                    {
                        currentPos.y += 1;
                        nextTile = PipeDB[Pipe.RD - adjustment];

                        //In the case there is multiple tiles to the right
                        //Check to see if previous tile was the horizontal tile
                        if (tileList[count - 2] == PipeDB[Pipe.Hori])
                        {
                            tileList[count - 1] = PipeDB[Pipe.LU + adjustment];
                        }
                        //If not place the vertical tile
                        else
                        {
                            tileList[count - 1] = PipeDB[Pipe.Vert];
                        }
                    }
                    //If there is, move down
                    else if (!tileMap.GetTile(nextPos + Vector3Int.down))
                    {
                        currentPos.y -= 1;
                        nextTile = PipeDB[Pipe.RU - adjustment];

                        //In the case there is multiple tiles to the right
                        //Check to see if previous tile was the horizontal tile
                        if (tileList[count - 2] == PipeDB[Pipe.Hori])
                        {
                            tileList[count - 1] = PipeDB[Pipe.LD + adjustment];
                        }
                        //If not place the vertical tile
                        else
                        {
                            tileList[count - 1] = PipeDB[Pipe.Vert];
                        }
                    }
                    else
                    {
                        Debug.LogError("Could not find a path. Please change Start and/or End points");
                        break;
                    }
                }
                else
                {
                    currentPos.x += MoveTowardsInt(currentPos.x, endPosR.x);
                }

                Positions.Add(currentPos);
                tileList.Add(nextTile);
                nextPos = currentPos;

                count++;
            }

            //Turning towards the end pos
            nextPos = currentPos;
            //Current pos needs to head downwards
            if (currentPos.y > endPosR.y)
            {
                //if we start to the left of the end point
                if (startPosR.x < endPosR.x)
                    tileList[count - 1] = PipeDB[Pipe.LD];
                //if we start to the right of the end point
                else
                    tileList[count - 1] = PipeDB[Pipe.RD];
            }
            //Current pos needs to upwards
            else
            {
                //if we start to the left of the end point
                if (startPosR.x < endPosR.x)
                    tileList[count - 1] = PipeDB[Pipe.LU];
                //if we start to the right of the end point
                else
                    tileList[count - 1] = PipeDB[Pipe.RU];
            }


            //Updating the Y position
            while (currentPos.y != endPosR.y)
            {
                //This is for when am moving up instead of down
                int adjustment = 0;
                if (currentPos.y > endPosR.y)
                    adjustment = 1;

                TileBase nextTile = PipeDB[Pipe.Vert];
                nextPos.y += MoveTowardsInt(currentPos.y, endPosR.y);
                if (tileMap.GetTile(nextPos) && !ignoreTilemap)
                {
                    nextPos = currentPos;
                    //If there is not a tile to the right, move current pos right
                    if (!tileMap.GetTile(nextPos + Vector3Int.right))
                    {
                        currentPos.x += 1;
                        nextTile = PipeDB[Pipe.LU + adjustment];

                        if (tileList[count - 2] == PipeDB[Pipe.Vert])
                        {
                            tileList[count - 1] = PipeDB[Pipe.RD - adjustment];
                        }
                        else
                        {
                            tileList[count - 1] = PipeDB[Pipe.Hori];
                        }
                    }
                    //If there is, move left
                    else if (!tileMap.GetTile(nextPos + Vector3Int.right))
                    {
                        currentPos.x -= 1;
                        nextTile = PipeDB[Pipe.RU + adjustment];

                        if (tileList[count - 2] == PipeDB[Pipe.Vert])
                        {
                            tileList[count - 1] = PipeDB[Pipe.LD - adjustment];
                        }
                        else
                        {
                            tileList[count - 1] = PipeDB[Pipe.Hori];
                        }
                    }
                    else
                    {
                        Debug.LogError("Could not find a path. Please change Start and/or End points");
                        break;
                    }
                }
                else
                {
                    currentPos.y += MoveTowardsInt(currentPos.y, endPosR.y);
                }

                Positions.Add(currentPos);
                tileList.Add(nextTile);
                nextPos = currentPos;

                count++;
            }
            loops++;
        }

        if (currentPos != endPosR && loops >= 5)
        {
            Debug.LogError("Could not get to end point: " + endPosR + ". Reasons for why that might be:\n" +
                "- Can not get to end point because it is through tile map, thus inaccessible\n" +
                "- It was very far and exceded the number of loops\n");
        }

        Vector3[] posArray = Array.ConvertAll(Positions.ToArray(), item => (Vector3)item);
        line.positionCount = Positions.Count;
        line.SetPositions(posArray);

        cableTileMap.SetTiles(Positions.ToArray(), tileList.ToArray());

        Positions.Clear();
        tileList.Clear();
    }

    //Adds 1 or -1 based on the dist from vectors a to b
    int MoveTowardsInt(float a, float b)
    {
        float dist = a - b;

        if(dist < 0)
        {
            return 1;
        }
        else if(dist > 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 sPint = Vector3Int.FloorToInt(startPos.transform.position);
        Vector3 ePint = Vector3Int.FloorToInt(endPos.transform.position);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(sPint + new Vector3(0.5f, 0.5f, 0), new Vector3(1, 1, 0));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ePint + new Vector3(0.5f, 0.5f, 0), new Vector3(1, 1, 0));
    }
}
