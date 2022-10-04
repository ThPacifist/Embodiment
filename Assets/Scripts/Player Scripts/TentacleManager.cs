using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleManager : MonoBehaviour
{
    public static TentacleManager instance;

    public Rigidbody2D rb;
    public BasicMovement basicMovement;
    [Range(0f, 1f)]
    public float influence = 0.5f;
    public List<TentacleDrawer> tentacles;
    public Vector3 size;
    public Vector3 offset;
    public float randDist = 1.5f;
    [HideInInspector]
    public Bounds bounds;
    //[HideInInspector]
    public RaycastHit2D ray;

    LinkedList<TentacleDrawer> tentaclesLL;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        Vector2 temp = rb.velocity;

        bounds.center = transform.position + offset + new Vector3(
            Mathf.Clamp(temp.x, -2, 2)*influence, 
            0, 
            0);
        bounds.size = size + new Vector3(
            Mathf.Abs(Mathf.Clamp(temp.x, -2, 2)*2*influence), 
            0, 
            0);

        int layer = LayerMask.GetMask("Jumpables");

        ray = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, layer);
    }

    public void UpdateTentaclePosition(TentacleDrawer tent)
    {
        TentacleDrawer temp = tentacles.Find(t => t.name == tent.name);
        Debug.Log("Called by " + temp.name);

        int layer = LayerMask.GetMask("Jumpables");
        float value = 0;
        float rand = Random.Range(0, randDist);

        if(basicMovement.direction == BasicMovement.Direction.Left)
        {
            value = -bounds.extents.x + rand;
        }
        else
        {
            value = bounds.extents.x - rand;
        }

        //Raycast is sent from the left of the bounds down to the bottom
        RaycastHit2D hit = Physics2D.Raycast(new Vector2((bounds.center.x + value), bounds.center.y),
            Vector2.down, bounds.extents.y, layer);

        Debug.DrawRay(new Vector2((bounds.center.x + value), bounds.center.y), Vector3.down, Color.red, 5);

        if (hit.collider == null)
        {
            hit = Physics2D.Raycast(bounds.min, Vector2.right, bounds.extents.x / 2, layer);
        }

        if (hit.collider != null)
        {
            temp.AnimateTransform(hit.point);
            return;
        }

        Debug.LogWarning("Could not find a surface");
    }   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 temp = rb.velocity;
        Vector3 currentCenter = transform.position + offset + new Vector3(
                Mathf.Clamp(temp.x, -2, 2) * influence,
                0,
                0);
        Vector3 currentSize = size + new Vector3(
                Mathf.Abs(Mathf.Clamp(temp.x, -2, 2) * 2 * influence),
                0,
                0);

        Gizmos.DrawWireCube(currentCenter, currentSize);

        Vector3 minPoint = currentCenter - (currentSize/2);
        Vector3 maxPoint = currentCenter + (currentSize/2);
        Vector3 leftLine = new Vector3(minPoint.x + (randDist), minPoint.y, 0f);
        Vector3 rightLine = new Vector3(maxPoint.x - (randDist), minPoint.y, 0f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(leftLine, new Vector3(leftLine.x, maxPoint.y, 0f));
        Gizmos.DrawLine(rightLine, new Vector3(rightLine.x, maxPoint.y, 0f));
    }
}
