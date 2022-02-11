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
    Vector3 currentPos;
    [SerializeField]
    GameObject startPos;
    [SerializeField]
    GameObject endPos;
    [SerializeField]
    Tilemap tileMap;

    Vector3 startPosR;
    Vector3 endPosR;
    List<Vector3> Positions = new List<Vector3>();

    public void GenerateCable()
    {
        Positions.Clear();
        //Converts the positions of the game objects to be integer vectors
        startPosR = Vector3Int.FloorToInt(startPos.transform.position) + new Vector3(0.5f, 0.5f, 0);
        endPosR = Vector3Int.FloorToInt(endPos.transform.position) + new Vector3(0.5f, 0.5f, 0);
        Debug.Log("Start Pos is " + startPosR + ". End pos is " + endPosR + ".");
        //Initializes starting position
        currentPos = startPosR;
        Positions.Add(currentPos);

        while(currentPos.x != endPosR.x)
        {
            currentPos.x += MoveTowardsInt(currentPos.x, endPosR.x);
            Positions.Add(currentPos);
        }

        while (currentPos.y != endPosR.y)
        {
            currentPos.y += MoveTowardsInt(currentPos.y, endPosR.y);
            Positions.Add(currentPos);
        }

        for(int i = 0; i < Positions.Count; i++)
        {
            Debug.Log("Position " + i + " is: " + Positions[i]);
        }

        line.positionCount = Positions.Count;
        line.SetPositions(Positions.ToArray());

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
        Vector3 sPint = Vector3Int.FloorToInt(startPos.transform.position) + new Vector3(0.5f, 0.5f, 0);
        Vector3 ePint = Vector3Int.FloorToInt(endPos.transform.position) + new Vector3(0.5f, 0.5f, 0);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(sPint, 1);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ePint, 1);
    }
}
