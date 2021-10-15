using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EmbodyField : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D myColliderInput;
    [SerializeField]
    Tilemap tileMap;

    public bool safe = false;

    private void Start()
    {
        //myColliderInput.enabled = false;
    }

    public bool CheckSpace()
    {
        safe = true;
        //Debug.Log(Vector2Int.FloorToInt(myColliderInput.bounds.min));
        Vector2Int highBoundsInt = Vector2Int.FloorToInt(myColliderInput.bounds.max);
        Vector2Int lowBoundsInt = Vector2Int.FloorToInt(myColliderInput.bounds.min);

        for(int i = lowBoundsInt.x - 1 ; i < highBoundsInt.x + 1; i++)
        {
            for(int j = lowBoundsInt.y; j < highBoundsInt.y + 1; j++)
            {
                //Debug.Log("i is " + (i) + " , j is " + (j));
                if(tileMap.GetTile(new Vector3Int(i, j, 0)))
                {
                    safe = false;
                    //Debug.Log("This is not safe");
                }
            }
        }

        return safe;
    }
}
