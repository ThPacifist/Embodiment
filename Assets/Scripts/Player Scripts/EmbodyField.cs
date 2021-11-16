using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EmbodyField : MonoBehaviour
{
    [SerializeField]
    Tilemap tileMap;

    public bool safe = false;
    Bounds nextBounds;
    Vector3Int hitPos;

    public bool CheckSpace(Vector3 floor, SkeletonTrigger to)
    {
        safe = true;
        GameObject test = new GameObject("test");
        nextBounds = new Bounds(floor + new Vector3(0, to.colliderSize.y / 2, 0), to.colliderSize);

        test.transform.position = nextBounds.center;

        Vector2Int highBoundsInt = Vector2Int.FloorToInt(nextBounds.max);
        Vector2Int lowBoundsInt = Vector2Int.FloorToInt(nextBounds.min);


        for (int i = lowBoundsInt.x; i < highBoundsInt.x + 1; i++)
        {
            for(int j = lowBoundsInt.y; j < highBoundsInt.y + 1; j++)
            {
                //Debug.Log("i is " + (i) + " , j is " + (j));
                if(tileMap.GetTile(new Vector3Int(i + Mathf.FloorToInt(tileMap.transform.localPosition.x), j + Mathf.FloorToInt(tileMap.transform.localPosition.y), 0)))
                {

                    safe = false;
                    Debug.Log("You hit a tile at: " + new Vector3Int(i, j, 0));
                    Debug.Log("You hit: " + tileMap.GetTile(new Vector3Int(i, j, 0)));
                    hitPos = new Vector3Int(i, j, 0);
                }
            }
        }

        return safe;
    }

    private void OnDrawGizmos()
    {
        Vector3Int highBoundsInt = Vector3Int.FloorToInt(nextBounds.max);
        Vector3Int lowBoundsInt = Vector3Int.FloorToInt(nextBounds.min);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(highBoundsInt + new Vector3(1, 1, -1), new Vector3Int(highBoundsInt.x + 1, lowBoundsInt.y, -1));
        Gizmos.DrawLine(new Vector3Int(highBoundsInt.x + 1, lowBoundsInt.y, -1), lowBoundsInt + new Vector3(0, 0, -1));
        Gizmos.DrawLine(lowBoundsInt + new Vector3(0, 0, -1), new Vector3Int(lowBoundsInt.x, highBoundsInt.y + 1, -1));
        Gizmos.DrawLine(new Vector3Int(lowBoundsInt.x, highBoundsInt.y + 1, -1), highBoundsInt + new Vector3(1, 1, -1));

        if (hitPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitPos, 0.5f);
        }
    }
}
