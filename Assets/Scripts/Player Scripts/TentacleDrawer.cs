using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class TentacleDrawer : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] int Smoothing = 20;
    [SerializeField] Transform body;
    public float radius;
    public float range;
    Vector3[] Points;

    // Start is called before the first frame update
    void Start()
    {
        Points = new Vector3[Smoothing+1];

        line.positionCount = Smoothing+1;

        DrawTentacle();
    }

    private void Update()
    {
        DrawTentacle();
    }

    void DrawTentacle()
    {
        float x = Mathf.Clamp(transform.position.x - (transform.localPosition.x * range), body.position.x - radius, body.position.x + radius);
        float y = -1 * Mathf.Sqrt(Mathf.Abs(Mathf.Pow(radius, 2) - Mathf.Pow((x - body.position.x), 2))) + body.position.y;
        Vector3 bodyPos = new Vector3(x, y, 0);

        for (int i = 0; i <= Smoothing; i++)
        {
            Points[i] = CubicLerp(body.position, bodyPos, transform.position + transform.up / 2, transform.position, ((float)i / Smoothing));
        }

        line.SetPositions(Points);
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

        float x = Mathf.Clamp(transform.position.x - (transform.localPosition.x * range), body.position.x - radius, body.position.x + radius);
        float y = -Mathf.Sqrt(Mathf.Abs(Mathf.Pow(radius, 2) - Mathf.Pow(x - body.position.x, 2))) + body.position.y;
        Vector3 bodyPos = new Vector3(x, y, 0);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(bodyPos, 0.2f);
    }

}
