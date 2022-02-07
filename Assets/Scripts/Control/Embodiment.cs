using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Embody()
    {
        Debug.Log("Embody");
        if(targetSkeleton != null && canEmbody)
        {
            //Enables the controller of the targeted form
            BlobController temp = (BlobController)PlayerBrain.Skeletons[PlayerBrain.skeleType.Blob];
            temp.PickUpSkeleton(targetSkeleton);
            PlayerBrain.PB.currentController.enabled = false;
            PlayerBrain.Skeletons[targetSkeleton.type].enabled = true;
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

    void Disembody()
    {
        Debug.Log("Disembody");
        if (currentSkeleton != null)
        {
            //Disables the controller of the old form and renables Blob form
            BlobController temp = (BlobController)PlayerBrain.Skeletons[PlayerBrain.skeleType.Blob];
            temp.PickUpSkeleton(null);
            PlayerBrain.PB.currentController.enabled = false;
            PlayerBrain.Skeletons[PlayerBrain.skeleType.Blob].enabled = true;
            Debug.Log("Player set to " + targetSkeleton.type);

            //Renables skeleton
            currentSkeleton.gameObject.SetActive(true);
            currentSkeleton.parent = null;
            currentSkeleton = null;

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
