using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("LBox") || other.collider.CompareTag("MBox") || other.collider.CompareTag("HBox"))
        {
            other.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("LBox") || other.collider.CompareTag("MBox") || other.collider.CompareTag("HBox"))
        {
            if (other.transform.parent == transform)
            {
                other.transform.parent = null;
            }
        }
    }
}
