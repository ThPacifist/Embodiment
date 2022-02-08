using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is the new Control Movement
/// Note: Whenever we want to prevent the player from emboding or disemboding somewhere, set canEmbody or canDisembody to false
///     but make sure you also set it back to true. Otherwise, the player will never be able to leave it's form
/// </summary>
public class Embodiment : MonoBehaviour
{
    public Transform currentSkeleton;
    public SkeletonTrigger targetSkeleton;
    public static bool canEmbody = true;
    public static bool canDisembody = false;

    private void OnEnable()
    {
        Controller.Embody += Embody;
    }

    private void OnDisable()
    {
        Controller.Embody -= Embody;
        Controller.Embody -= Disembody;
    }

    //Changes the player's form
    void Embody()
    {
        Debug.Log("Embody");
        if(targetSkeleton != null && canEmbody)
        {
            //CheckSpace(targetSkeleton);
            //Enables the controller of the targeted form
            PlayerBrain.PB.currentController.enabled = false;
            PlayerBrain.Skeletons[targetSkeleton.type].enabled = true;
            targetSkeleton.isGrabbed = true;
            Debug.Log("Player set to " + targetSkeleton.type);

            //Attach skeleton to player and disable it
            currentSkeleton = targetSkeleton.transform.parent;
            currentSkeleton.parent = transform;
            currentSkeleton.gameObject.SetActive(false);

            Controller.Embody -= Embody;
            canEmbody = false;
            Controller.Embody += Disembody;
            Debug.Log("Can Disembody");
            canDisembody = true;
        }
    }

    //Reverts the Player back to the blob form
    void Disembody()
    {
        Debug.Log("Disembody");
        if (currentSkeleton != null && canDisembody)
        {
            //Disables the controller of the old form and renables Blob form
            BlobController temp = (BlobController)PlayerBrain.Skeletons[PlayerBrain.skeleType.Blob];
            PlayerBrain.PB.currentController.enabled = false;
            PlayerBrain.Skeletons[PlayerBrain.skeleType.Blob].enabled = true;
            Debug.Log("Player set to " + targetSkeleton.type);

            //Renables skeleton
            currentSkeleton.gameObject.SetActive(true);
            currentSkeleton.parent = null;
            currentSkeleton = null;
            temp.PickUpSkeleton(targetSkeleton);
            PlayerBrain.PB.plyAnim.SetTrigger("Disembody");

            Controller.Embody += Embody;
            canEmbody = true;
            Controller.Embody -= Disembody;
            canDisembody = false;
        }
    }

    public void SetTargetSkeleton(SkeletonTrigger target)
    {
        targetSkeleton = target;
    }

    //Checks if theres is enough space for the next form
    bool CheckSpace(SkeletonTrigger target)
    {
        //Controller of the skeleton the player is transforming into
        Controller targetSkeleton = PlayerBrain.Skeletons[target.type];
        //Calculate the center of the collider after transforming
        Vector2 targetCenter = new Vector2(PlayerBrain.PB.plyCol.bounds.center.x, 
            PlayerBrain.PB.plyCol.bounds.min.y + targetSkeleton.colliderSize.y);

        //Send a Capsule cast to see if there is enough space for the player
        RaycastHit2D hit = Physics2D.CapsuleCast(targetCenter, targetSkeleton.colliderSize, targetSkeleton.direction,
            0f, Vector2.down, 0f, PlayerBrain.PB.groundLayerMask);

        Debug.Log("Hit is " + hit.collider.name);
        return true;
    }
}
