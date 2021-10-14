using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbodyField : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D myColliderInput;

    float min = 0.0001f;
    float max = 2.25f;
    bool notTouching = false;
    static CircleCollider2D myCol;

    private void Start()
    {
        myCol = myColliderInput;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public static void ExpandField()
    {
        
    }

    IEnumerator expandField()
    {
        yield return null;
    }
}
