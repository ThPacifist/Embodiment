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
        Horizontal,
        Vertical,
        LeftUp,
        LeftDown,
        RightUp,
        RightDown
    }

    [SerializeField]
    LineRenderer line;
    Vector3Int currentPos;
    [SerializeField]
    GameObject startPos;
    [SerializeField]
    GameObject endPos;
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

        PipeDB.Add(Pipe.Horizontal, HorizontalCable);
        PipeDB.Add(Pipe.Vertical, VerticalCable);
        PipeDB.Add(Pipe.LeftUp, leftUp);
        PipeDB.Add(Pipe.LeftDown, leftDown);
        PipeDB.Add(Pipe.RightUp, rightUp);
        PipeDB.Add(Pipe.RightDown, rightDown);

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
        tileList.Add(PipeDB[Pipe.Horizontal]);
        count = 1;

        while(currentPos.x != endPosR.x)
        {
            //This is for when I am moving right instead of left
            int adjustment = 0;
            if (currentPos.x > endPosR.x)
                adjustment = 2;

            //Sets the default tile
            TileBase nextTile = PipeDB[Pipe.Horizontal];
            nextPos.x += MoveTowardsInt(currentPos.x, endPosR.x);
            if (tileMap.GetTile(nextPos))
            {
                Debug.Log("There is a tile");
                nextPos = currentPos;
                //If there is not a tile above, move current pos up
                if(!tileMap.GetTile(nextPos + Vector3Int.up))
                {
                    currentPos.y += 1;
                    //tileList[count - 1] = PipeDB[Pipe.LeftUp + adjustment];
                    //nextTile = PipeDB[Pipe.RightDown - adjustment];
                }
                //If there is, move down
                else if(!tileMap.GetTile(nextPos + Vector3Int.down))
                {
                    currentPos.y -= 1;
                    //tileList[count - 1] = PipeDB[Pipe.LeftDown + adjustment];
                    //nextTile = PipeDB[Pipe.RightUp - adjustment];
                }
                else
                {
                    Debug.LogError("Could not find a path. Please change Start and/or End points");
                    break;
                }
            }
            else
            {
                Debug.Log("There is not a tile");
                currentPos.x += MoveTowardsInt(currentPos.x, endPosR.x);
            }

            Positions.Add(currentPos);
            tileList.Add(nextTile);
            nextPos = currentPos;

            count++;
        }

        nextPos = currentPos;
        /*if(currentPos.y > endPosR.y)
        {
            tileList[count - 1] = PipeDB[Pipe.LeftDown];
        }
        else
        {
            tileList[count - 1] = PipeDB[Pipe.LeftUp];
        }*/

        while (currentPos.y != endPosR.y)
        {
            TileBase nextTile = PipeDB[Pipe.Vertical];
            nextPos.y += MoveTowardsInt(currentPos.y, endPosR.y);
            if (tileMap.GetTile(nextPos))
            {
                Debug.Log("There is a tile");
                nextPos = currentPos;
                //If there is not a tile to the right, move current pos up
                if (!tileMap.GetTile(nextPos + Vector3Int.right))
                {
                    currentPos.x += 1;
                }
                //If there is, move left
                else if(!tileMap.GetTile(nextPos + Vector3Int.right))
                {
                    currentPos.x -= 1;
                }
                else
                {
                    Debug.LogError("Could not find a path. Please change Start and/or End points");
                    break;
                }
            }
            else
            {
                Debug.Log("There is not a tile");
                currentPos.y += MoveTowardsInt(currentPos.y, endPosR.y);
            }

            Positions.Add(currentPos);
            tileList.Add(nextTile);
            nextPos = currentPos;

            count++;
        }

        /*for(int i = 0; i < Positions.Count; i++)
        {
            Debug.Log("Position " + i + " is: " + Positions[i]);
        }*/
        //if (count >= 50)
        //{

        Vector3[] posArray = Array.ConvertAll(Positions.ToArray(), item => (Vector3)item);
        line.positionCount = Positions.Count;
        line.SetPositions(posArray);
        //}

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
