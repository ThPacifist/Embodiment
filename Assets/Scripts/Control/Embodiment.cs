using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embodiment : MonoBehaviour
{
    public Transform heldSkeleton;
    public PlayerData plyData;

    SkeletonTrigger targetSkeleton;

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
        if(targetSkeleton != null)
        {

        }
    }

    void Disembody()
    {

    }

    void SetTargetSkeleton(SkeletonTrigger target)
    {
        targetSkeleton = target;
    }

    bool CheckSpace()
    {
        return true;
    }
}
