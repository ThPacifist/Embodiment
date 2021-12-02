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
            if (plyCntrl != null)
            {
                plyCntrl.SetCatOnWall(true, direction);
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
    }
}
