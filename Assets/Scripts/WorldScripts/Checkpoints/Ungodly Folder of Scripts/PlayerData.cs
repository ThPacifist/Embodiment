using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : AntiChrist
{
    //Variables
    public GameObject player;
    public SpecialInteractions spcInt;
    public ControlMovement ctrlMvm;
    private Transform heldSkeleton;
    private Rigidbody2D heldBox;
    private string pTag;
    private bool objectHeld;


    [SerializeField]
    FixedJoint2D fixedJ;


    //Constructor
    public override void Constructor()
    {
        pTag = player.tag;
        heldBox = spcInt.heldBox;
        heldSkeleton = ctrlMvm.heldSkeleton;
        objectHeld = spcInt.objectHeld;
    }
    //Rebuild Data
    public override void RebuildData()
    {
        pTag = player.tag;
        heldBox = spcInt.heldBox;
        heldSkeleton = ctrlMvm.heldSkeleton;
        objectHeld = spcInt.objectHeld;
    }

    //Reset Data
    public override void ResetData()
    {
        //If the player should be embodied
        if(!player.CompareTag(pTag))
        {
            //Drop any current skeleton at time of death
            if(ctrlMvm.heldSkeleton != null)
            {
                ctrlMvm.heldSkeleton.gameObject.SetActive(true);
            }
            //Grab skeleton they should be holding
            if(pTag != "Blob")
            {

            }
            //Change player
        }
        //If the player should be holding a box
        if(objectHeld != spcInt.objectHeld)
        {
            spcInt.objectHeld = objectHeld;
            spcInt.heldBox = heldBox;
            fixedJ.enabled = true;
            fixedJ.connectedBody = heldBox;
        }
    }
}
