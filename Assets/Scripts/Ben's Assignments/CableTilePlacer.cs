using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileMapColorChanger))]
public class CableTilePlacer : MonoBehaviour
{
    Vector2Int currentPos;
    [SerializeField]
    GameObject startPos;
    [SerializeField]
    GameObject endPos;

    Vector2Int startPosInt;
    Vector2Int endPosInt;

    void GenerateCable()
    {
        //Converts the positions of the game objects to be integer vectors
        startPosInt = Vector2Int.FloorToInt(startPos.transform.position);
        endPosInt = Vector2Int.FloorToInt(endPos.transform.position);
        //Initializes starting position
        currentPos = startPosInt;

        while(currentPos.x > endPosInt.x)
        {
            currentPos.x += MoveTowardsInt(currentPos, endPosInt);
        }

        Debug.Log("X position is now the same");
    }

    //Shorthand for Vector2Int.FloorToInt
    Vector2Int FlInt(Vector2 input)
    {
        return Vector2Int.FloorToInt(input);
    }

    int MoveTowardsInt(Vector2 a, Vector2 b)
    {
        float dist = Vector2.Distance(a, b);

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
        Vector3Int sPint = Vector3Int.FloorToInt(startPos.transform.position);
        Vector3Int ePint = Vector3Int.FloorToInt(endPos.transform.position);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(sPint, Vector3.one * 0.3f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ePint, Vector3.one * 0.3f);
    }
}
