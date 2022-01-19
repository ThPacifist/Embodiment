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
    public Transform heldSkeleton;
    public SkeletonTrigger skeleton;
    private string pTag;
    private bool objectHeld;
    private bool skelHeld;
    private bool hBoxHeld;


    [SerializeField]
    FixedJoint2D fixedJ;


    //Constructor
    public override void Constructor()
    {
        pTag = player.tag;
        heldSkeleton = ctrlMvm.heldSkeleton;
        objectHeld = spcInt.objectHeld;
        skelHeld = spcInt.skelHeld;
        hBoxHeld = spcInt.HboxHeld;
        if(heldSkeleton != null)
        {
            skeleton = heldSkeleton.GetChild(0).GetComponent<SkeletonTrigger>();
        }
    }
    //Rebuild Data
    public override void RebuildData()
    {
        pTag = player.tag;
        heldSkeleton = ctrlMvm.heldSkeleton;
        objectHeld = spcInt.objectHeld;
        skelHeld = spcInt.skelHeld;
        hBoxHeld = spcInt.HboxHeld;
        if (heldSkeleton != null)
        {
            skeleton = heldSkeleton.GetChild(0).GetComponent<SkeletonTrigger>();
        }
    }

    //Reset Data
    public override void ResetData()
    {
        //If the player should be embodied
        if(heldSkeleton != ctrlMvm.heldSkeleton)
        {
            //Change sprite back to what it should be
            if(pTag != "Blob")
            {
                animPly.SetInteger("Form", skeleton.Form);
            }
            else
            {
                animPly.SetInteger("Form", 0);
            }
            
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
            if(player.tag != pTag && skeleton != null)
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
            else /*if(player.tag == "Blob")*/
            {
                ctrlMvm.DestorySkeleton();
            }
        }
        //If the player is holding anything, drop it
        if(spcInt.objectHeld || spcInt.skelHeld || spcInt.HboxHeld)
        {
            //Change bools to be correct
            spcInt.objectHeld = false;
            spcInt.skelHeld = false;
            spcInt.HboxHeld = false;

            //Make the player hold nothing
            spcInt.heldSkel = null;
            spcInt.skelHeld = false;
            fixedJ.enabled = false;
            fixedJ.connectedBody = null;
            animPly.SetBool("isGrabbing", false);
            plyCtrl.jumpHeight = 18.1f;
        }
        //Reset pick up target
        spcInt.box = null;
        spcInt.skeleton = null;

        //Get rid of interact indicators
        if(spcInt.prefabInstance != null)
        {
            Destroy(spcInt.prefabInstance);
        }

        //Reset current embody target
        if(ctrlMvm.skeleton != skeleton)
        {
            ctrlMvm.skeleton = skeleton;
        }

        //Reset heldskel
        if(spcInt.heldSkel != null)
        {
            spcInt.heldSkel.isGrabbed = false;
            spcInt.heldSkel = null;
        }

        //Reset heldBox
        if(spcInt.heldBox != null)
        {
            spcInt.heldBox = null;
        }
    }
}
