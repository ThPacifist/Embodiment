using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TileMapColorChanger))]
[RequireComponent(typeof(LineRenderer))]
public class CableTilePlacer : MonoBehaviour
{
    [SerializeField]
    LineRenderer line;
    Vector3Int currentPos;
    [SerializeField]
    GameObject startPos;
    [SerializeField]
    GameObject endPos;
    [SerializeField]
    Tilemap tileMap;

    Vector3Int startPosR;
    Vector3Int endPosR;
    //Serves as the next position of the loop
    Vector3Int nextPos;
    List<Vector3> Positions = new List<Vector3>();
    int count = 0;

    public void GenerateCable()
    {
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

        while(currentPos.x != endPosR.x && count < 100)
        {
            nextPos.x += MoveTowardsInt(currentPos.x, endPosR.x);
            if (tileMap.GetTile(nextPos))
            {
                Debug.Log("There is a tile");
                nextPos = currentPos;
                //If there is not a tile above, move current pos up
                if(!tileMap.GetTile(nextPos + Vector3Int.up))
                {
                    currentPos.y += 1;
                }
                //If there is, move down
                else
                {
                    currentPos.y -= 1;
                }
            }
            else
            {
                Debug.Log("There is not a tile");
                currentPos.x += MoveTowardsInt(currentPos.x, endPosR.x);
            }

            Positions.Add(currentPos);
            nextPos = currentPos;

            count++;
        }

        nextPos = currentPos;
        count = 0;

        while (currentPos.y != endPosR.y && count < 100)
        {
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
                else
                {
                    currentPos.x -= 1;
                }
            }
            else
            {
                Debug.Log("There is not a tile");
                currentPos.y += MoveTowardsInt(currentPos.y, endPosR.y);
            }

            Positions.Add(currentPos);
            count++;
        }

        /*for(int i = 0; i < Positions.Count; i++)
        {
            Debug.Log("Position " + i + " is: " + Positions[i]);
        }*/
        //if (count >= 50)
        //{
            line.positionCount = Positions.Count;
            line.SetPositions(Positions.ToArray());
        //}

        Positions.Clear();
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
