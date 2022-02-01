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

    [ContextMenu("Generate Cable")]
    void GenerateCable()
    {
        //Converts the positions of the game objects to be integer vectors
        startPosInt = Vector2Int.FloorToInt(startPos.transform.position);
        endPosInt = Vector2Int.FloorToInt(endPos.transform.position);
        Debug.Log("Start Pos is " + startPosInt + ". End pos is " + endPosInt + ".");
        //Initializes starting position
        currentPos = startPosInt;

        Debug.Log("End pos X: " + endPosInt.x);
        while(currentPos.x != endPosInt.x)
        {
            currentPos.x += MoveTowardsInt(currentPos, endPosInt);
        }

        Debug.Log("X position is now the same");

        Debug.Log("End pos Y: " + endPosInt.y);
        while (currentPos.y != endPosInt.y)
        {
            currentPos.y += MoveTowardsInt(currentPos, endPosInt);
        }

        Debug.Log("Y position is now the same");

        Debug.Log("Current position is " + currentPos);
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
            return -1;
        }
        else if(dist > 0)
        {
            return 1;
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
        Gizmos.DrawWireSphere(sPint, 1);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ePint, 1);
    }
}
