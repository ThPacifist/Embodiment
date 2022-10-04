using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
//[ExecuteAlways]
public class TentacleDrawer : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] int Smoothing = 20;
    [SerializeField] Transform body;
    //Distance the relBodyPos is from the body pos
    public float radius;
    public float range;
    public float minDist = 0.5f;
    public float maxDist = 0.5f;
    public Rigidbody2D rb;
    TentacleManager tentacleManager;
    Vector3[] Points;
    public bool isMoving = false;

    //default radius = 0.45f
    //default range = 0.34f

    private void Awake()
    {
        tentacleManager = TentacleManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        Points = new Vector3[Smoothing+1];

        line.positionCount = Smoothing+1;

        DrawTentacle();
    }

    private void Update()
    {
        float influencePos = body.position.x + tentacleManager.rb.velocity.x * 0.3f;
        Vector3 leftBound = new Vector3(influencePos + minDist, tentacleManager.ray.point.y);
        Vector3 rightBound = new Vector3(influencePos + maxDist, tentacleManager.ray.point.y);
        
        float xdiff = transform.position.x - tentacleManager.ray.point.x;

        if(Mathf.Abs(xdiff) <= Mathf.Abs(minDist) || Mathf.Abs(xdiff) >= Mathf.Abs(maxDist) && !isMoving)
        {
            Vector3 endPoint;
            if(tentacleManager.basicMovement.direction == BasicMovement.Direction.Left)
            {
                if(transform.localPosition.x < 0)
                {
                    endPoint = rightBound;
                }
                else
                {
                    endPoint = leftBound;
                }
            }
            else
            {
                if(transform.localPosition.x < 0)
                {
                    endPoint = leftBound;
                }
                else
                {
                    endPoint = rightBound;
                }
            }
            int layer = LayerMask.GetMask("Jumpables");
            RaycastHit2D hit = Physics2D.Raycast(endPoint + Vector3.up/2, Vector3.down, Mathf.Infinity, layer);
             
            AnimateTransform(new Vector3(hit.point.x, hit.point.y));
        }

        /*if (tentacleManager != null && !tentacleManager.bounds.Contains(line.GetPosition(Smoothing)) && !isMoving)
        {
            Debug.Log("Tentacle Outside bounds");
            tentacleManager.UpdateTentaclePosition(this);
        }
        else
        {
            DrawTentacle();
        }*/
        DrawTentacle();

    }

    //Update transform.position to move tentacle
    void DrawTentacle()
    {
        Vector2 localEndPos = transform.position - body.position;
        int adj = localEndPos.y > 0 ? 1 : -1;
        //Calculates second pos coming from the body
        float x = Mathf.Clamp(transform.position.x - (localEndPos.x * range), body.position.x - radius, body.position.x + radius);
        float y = adj * Mathf.Sqrt(Mathf.Abs(Mathf.Pow(radius, 2) - Mathf.Pow((x - body.position.x), 2))) + body.position.y;
        Vector3 relBodyPos = new Vector3(x, y, 0);

        for (int i = 0; i <= Smoothing; i++)
        {
            Points[i] = CubicLerp(body.position, relBodyPos, transform.position + transform.up / 2, transform.position, ((float)i / Smoothing));
        }

        line.SetPositions(Points);
    }

    public void AnimateTransform(Vector3 newPos, float newRadius = 0.45f, float angle = 0f)
    {
        //If is moving is false, don't try to animate the tentacles again
        if(!isMoving)
            StartCoroutine(AnimateTransformE(newPos, newRadius, angle));
    }

    IEnumerator AnimateTransformE(Vector3 newPos, float newRadius = 0.45f, float angle = 0f)
    {
        isMoving = true;
        Quaternion newAngle = Quaternion.Euler(0, 0, angle);
        float oldRadius = radius;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        float dist = Vector3.Distance(startPos, newPos);

        int frames = 4;
        int elapsedFrames = 0;
        // min frames 4 : min dist 0.25f
        // Min dist is the minimum dist can go before frames defaults to 4
        if (dist <= 0.35f)
        {
            frames = 4;
        }
        // max frames 14 : max dist 1.25f
        // Max dist is the maximum dist can go before frames defaults to 14
        else if(dist >= 1.25f)
        {
            frames = 14;
        }
        //add a fraction of the frames needed to get to 14 frames, based on the how much dist is
        // 1.25 - dist = part of the dist between 0.35 and 1.25
        // part / 0.9
        //If dist is between max and min, increase frames depending on dist
        else
        {
            float part = 1.25f - dist;
            float fraction = part / 0.9f;
            frames = 6 + (int)(8 * fraction);
        }

        float temp;
        Vector2 pos2;
        Vector2 pos3;

        if(tentacleManager.basicMovement.direction == BasicMovement.Direction.Left)
        {
            temp = 45;
            pos2 = new Vector3((startPos.x - dist / 2) + 0.1f, startPos.y + dist / 2, 0f);
            pos3 = new Vector3((startPos.x - dist / 2) - 0.1f, startPos.y + dist / 2, 0f);
        }
        else
        {
            temp = -45;
            pos2 = new Vector3((startPos.x + dist / 2) - 0.1f, startPos.y + dist / 2, 0f);
            pos3 = new Vector3((startPos.x + dist / 2) + 0.1f, startPos.y + dist / 2, 0f);
        }

        while (elapsedFrames != frames)
        {
            float ratio = (float)elapsedFrames / frames;

            //Updates tentacle position
            transform.position = CubicLerp(startPos, pos2, pos3, newPos, ratio);
            //Updates tentacle rotation
            //transform.rotation = Quaternion.Lerp(startRot, newAngle, ratio);
            //transform.rotation = Quaternion.Euler(0, 0, temp * Mathf.Sin(6.4f * ratio));
            radius = oldRadius + (newRadius - oldRadius) * ratio;

            elapsedFrames = (elapsedFrames + 1) % (frames + 1);

            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);

        isMoving = false;
    }

    public Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, t);
    }

    public Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab_bc = QuadraticLerp(a, b, c, t);
        Vector3 bc_cd = QuadraticLerp(b, c, d, t);

        return Vector3.Lerp(ab_bc, bc_cd, t);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + (transform.up/2), 0.2f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(body.position, radius);

        int adj = transform.localPosition.y > 0 ? 1 : -1;
        float x = Mathf.Clamp(transform.position.x - (transform.localPosition.x * range), body.position.x - radius, body.position.x + radius);
        float y = adj * Mathf.Sqrt(Mathf.Abs(Mathf.Pow(radius, 2) - Mathf.Pow(x - body.position.x, 2))) + body.position.y;
        Vector3 bodyPos = new Vector3(x, y, 0);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(bodyPos, 0.2f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(body.position.x, transform.position.y + 0.05f, 0f), 
            new Vector3(body.position.x + minDist, transform.position.y + 0.05f, 0f));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(body.position.x + minDist, transform.position.y + 0.05f, 0f),
            new Vector3(body.position.x + maxDist, transform.position.y + 0.05f, 0f));

    }

}
