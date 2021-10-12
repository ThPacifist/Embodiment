using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShriekerField : MonoBehaviour
{
    [SerializeField]
    GameAction behavior;

    //If the player is this field, destory the skeleton
    private void OnTriggerEnter(Collider other)
    {
        if(GameAction.PlayerTags(other.tag))//If it's the player
        {
            DestroySkeleton(other.GetComponent<ControlMovement>());
            //Destroy Skeleton

            //Any other behavior
            //behavior.Action();
        }
    }

    void DestroySkeleton(ControlMovement ply)
    {
        ply.DestorySkeleton();
    }
}
