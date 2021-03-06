using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbTrigger : MonoBehaviour
{
    [Tooltip ("Turn this on if Climbable wall is to the left")]
    public bool left = false;
    [Tooltip("Turn this on if Climbable wall is to the right")]
    public bool right = false;

    [SerializeField]
    PlyController plyCntrl;
    //Code for Player Rework
    CatController cat;

    Vector2 direction;

    private void Awake()
    {
        //Sets direction gravity will be imposed on the cat
        if(right)
        {
            direction = Vector2.right;
        }
        else if(left)
        {
            direction = Vector2.left;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            plyCntrl = collision.GetComponent<PlyController>();
            cat = collision.GetComponent<CatController>();
            if (plyCntrl != null)
            {
                plyCntrl.SetCatOnWall(true, direction);
            }
            //Code for Player Rework
            if (cat != null)
            {
                cat.SetCatOnWall(true, direction);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (plyCntrl != null)
        {
            plyCntrl.SetCatOnWall(false, Vector2.zero);
            plyCntrl = null;
        }
        //Code for Player Rework
        if (cat != null)
        {
            cat.SetCatOnWall(false, Vector2.zero);
            cat = null;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 center = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(center + new Vector3(-0.5f, 0, 0), center + new Vector3(0.5f, 0, 0));
        if (right)
        {
            Vector3 tip = center + new Vector3(0.5f, 0, 0);
            Gizmos.DrawLine(tip, tip + new Vector3(-0.25f, 0.25f, 0));
            Gizmos.DrawLine(tip, tip + new Vector3(-0.25f, -0.25f, 0));
            Gizmos.DrawLine(tip + new Vector3(-0.25f, 0.25f, 0), tip + new Vector3(-0.25f, -0.25f, 0));
        }
        else if (left)
        {
            Vector3 tip = center + new Vector3(-0.5f, 0, 0);
            Gizmos.DrawLine(tip, tip + new Vector3(0.25f, 0.25f, 0));
            Gizmos.DrawLine(tip, tip + new Vector3(0.25f, -0.25f, 0));
            Gizmos.DrawLine(tip + new Vector3(0.25f, 0.25f, 0), tip + new Vector3(0.25f, -0.25f, 0));
        }
    }
}
