using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embodiment : MonoBehaviour
{
    public Transform heldSkeleton;
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

    void Embody()
    {
        if(targetSkeleton != null && canEmbody)
        {
            //Enables the controller of the targeted form
            BlobController temp = (BlobController)PlayerBrain.Skeletons[PlayerBrain.skeleType.Blob];
            temp.PickUpSkeleton(targetSkeleton);
            PlayerBrain.PB.currentController.enabled = false;
            PlayerBrain.Skeletons[targetSkeleton.type].enabled = true;
            Debug.Log("Player set to " + targetSkeleton.type);

            //Attach skeleton to player and disable it
            heldSkeleton = targetSkeleton.transform.parent;
            heldSkeleton.parent = transform;
            heldSkeleton.gameObject.SetActive(false);

            Controller.Embody -= Embody;
            canEmbody = false;
            Controller.Embody += Disembody;
            canDisembody = true;
        }
    }

    void Disembody()
    {
        if(heldSkeleton != null)
        {
            BlobController temp = (BlobController)PlayerBrain.Skeletons[PlayerBrain.skeleType.Blob];
            temp.PickUpSkeleton(null);
            PlayerBrain.PB.currentController.enabled = false;
            PlayerBrain.Skeletons[PlayerBrain.skeleType.Blob].enabled = true;
            Debug.Log("Player set to " + targetSkeleton.type);

            //Renables skeleton
            heldSkeleton.gameObject.SetActive(true);
            heldSkeleton.parent = null;

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

    bool CheckSpace()
    {
        return true;
    }
}
