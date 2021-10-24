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
            Debug.Log("Player is " + other.tag);
            if (other.GetComponent<ControlMovement>().heldSkeleton != null)
            {
                DestroySkeleton(other.GetComponent<ControlMovement>());
                //Destroy Skeleton
            }

            //Any other behavior
            //behavior.Action();
        }
    }

    void DestroySkeleton(ControlMovement ply)
    {
        Debug.Log("This is the Shrieker . " + ply);
        ply.DestorySkeleton();
    }
}
