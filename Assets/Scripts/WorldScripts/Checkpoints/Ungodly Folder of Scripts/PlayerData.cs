using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : AntiChrist
{
    //Variables
    public GameObject player;
    public SpecialInteractions spcInt;
    public ControlMovement ctrlMvm;
    public PlyController plyCtrl;
    public CapsuleCollider2D plyCol;
    public Animator animPly;
    private Transform heldSkeleton;
    public SkeletonTrigger skeleton;
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
        if(heldSkeleton != null)
        {
            skeleton = heldSkeleton.GetChild(0).GetComponent<SkeletonTrigger>();
        }
    }
    //Rebuild Data
    public override void RebuildData()
    {
        pTag = player.tag;
        heldBox = spcInt.heldBox;
        heldSkeleton = ctrlMvm.heldSkeleton;
        objectHeld = spcInt.objectHeld;
        if (heldSkeleton != null)
        {
            skeleton = heldSkeleton.GetChild(0).GetComponent<SkeletonTrigger>();
        }
    }

    //Reset Data
    public override void ResetData()
    {
        //If the player should be embodied
        if(!player.CompareTag(pTag))
        {
            //Drop the current skeleton at time of death
            if(ctrlMvm.heldSkeleton != null && ctrlMvm.heldSkeleton != heldSkeleton)
            {
                ctrlMvm.heldSkeleton.parent = null;
                ctrlMvm.heldSkeleton.gameObject.SetActive(true);
                ctrlMvm.heldSkeleton = null;
            }
            //Select the correct skeleton
            if(ctrlMvm.heldSkeleton != heldSkeleton)
            {
                ctrlMvm.heldSkeleton = heldSkeleton;
                ctrlMvm.skeleton = skeleton;
                spcInt.heldSkel = skeleton;
                heldSkeleton.parent = player.transform;
                heldSkeleton.position = player.transform.position;
                heldSkeleton.gameObject.SetActive(false);
            }
            //Embody current skeleton if it isn't already
            if(player.tag != pTag)
            {
                //Changes players values to be the skeleton
                player.tag = skeleton.Name;
                plyCtrl.speed = skeleton.speed;
                plyCtrl.jumpHeight = skeleton.jumpHeight;
                plyCol.size = skeleton.colliderSize;
                plyCol.offset = skeleton.colliderOffset;
                plyCol.direction = skeleton.direction;
                plyCol.density = skeleton.density;

                //Changes players sprite to be the skeleton
                animPly.SetTrigger(skeleton.Name);
            }
        }
        //If the player should be holding a box
        if(objectHeld != spcInt.objectHeld)
        {
            spcInt.objectHeld = objectHeld;
            spcInt.heldBox = heldBox;
            fixedJ.enabled = true;
            fixedJ.connectedBody = heldBox;
            plyCol.size = skeleton.colliderSize;
            plyCol.offset = skeleton.colliderOffset;
        }
    }
}
