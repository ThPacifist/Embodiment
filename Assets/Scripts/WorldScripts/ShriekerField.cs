using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShriekerField : MonoBehaviour
{
    [SerializeField]
    GameAction behavior;

    //If the player is this field, destory the skeleton
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(GameAction.PlayerTags(other.tag))//If it's the player
        {
            if (other.gameObject.layer == 6)
            {
                ControlMovement temp = other.GetComponent<ControlMovement>();
                if (temp.heldSkeleton != null || temp.skeleton != null)
                {
                    DestroySkeleton(temp);
                }
            }

            //Any other behavior
            //behavior.Action();
        }
    }

    void DestroySkeleton(ControlMovement ply)
    {
        Debug.Log("Inside Destroy Skeleton");
        ply.DestorySkeleton();
    }
}
