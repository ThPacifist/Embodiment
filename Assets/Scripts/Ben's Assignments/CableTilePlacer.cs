using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TileMapColorChanger))]
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

        //Initializing TileMap
        GameObject CableMap = new GameObject("Cable Map");
        CableMap.transform.parent = this.transform.parent;
        Tilemap cableTileMap = CableMap.AddComponent<Tilemap>();
        CableMap.AddComponent<TilemapRenderer>();

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

        //Updating the x position
        while(currentPos.x != endPosR.x)
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
                if(!tileMap.GetTile(nextPos + Vector3Int.up))
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
                else if(!tileMap.GetTile(nextPos + Vector3Int.down))
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
        if(currentPos.y > endPosR.y)
        {
            int adjustment = 0;
            if (currentPos.x > endPosR.x)
                adjustment = 2;
            tileList[count - 1] = PipeDB[Pipe.LD + adjustment];
        }
        else
        {
            int adjustment = 0;
            if (currentPos.x > endPosR.x)
                adjustment = 2;
            tileList[count - 1] = PipeDB[Pipe.LU + adjustment];
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
                        Debug.Log("There was a vertical tile");
                        tileList[count - 1] = PipeDB[Pipe.RD - adjustment];
                    }
                    else
                    {
                        Debug.Log("There wasn't a vertical tile");
                        tileList[count - 1] = PipeDB[Pipe.Hori];
                    }
                }
                //If there is, move left
                else if(!tileMap.GetTile(nextPos + Vector3Int.right))
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
        Gizmos.DrawWireSphere(sPint, 1);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ePint, 1);
    }
}
