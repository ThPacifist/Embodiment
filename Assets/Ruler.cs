using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Ruler : MonoBehaviour
{
    [SerializeField] GameObject tail;
    public float dist = 0f;

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, tail.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, tail.transform.position);
    }
}
