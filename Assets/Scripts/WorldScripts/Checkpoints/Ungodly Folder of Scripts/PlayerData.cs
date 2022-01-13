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
    public SkeletonTrigger heldSkel;
    public SkeletonTrigger skeleton;
    private Rigidbody2D heldBox;
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
        heldBox = spcInt.heldBox;
        heldSkel = spcInt.heldSkel;
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
        Debug.Log("Rebuild data run");
        pTag = player.tag;
        heldBox = spcInt.heldBox;
        heldSkel = spcInt.heldSkel;
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
        if(!player.CompareTag(pTag))
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
        //If the player should be holding something else/nothing
        if(objectHeld || spcInt.objectHeld || skelHeld || spcInt.skelHeld || hBoxHeld || spcInt.HboxHeld)
        {
            //Change bools to be correct
            spcInt.objectHeld = objectHeld;
            spcInt.skelHeld = skelHeld;
            spcInt.HboxHeld = hBoxHeld;

            //Check if the player should be holding nothing
            if (heldBox == null && heldSkel == null)
            {
                //Make the player hold nothing
                spcInt.heldSkel = null;
                spcInt.heldSkel.isGrabbed = false;
                spcInt.skelHeld = false;
                fixedJ.enabled = false;
                fixedJ.connectedBody = null;
                animPly.SetBool("isGrabbing", false);
                plyCtrl.jumpHeight = 18.1f;
            }
            //Check if the player should be holding a box
            else if (heldBox != spcInt.heldBox)
            {
                //Make the player hold the box they should be holding
                Debug.Log("The player now holds the box " + heldBox);
            }
            // Check if the player should be holding a skeleton
            else if (heldSkel != spcInt.heldSkel)
            {
                //Make the player hold the skeleton they should be holding
                Debug.Log("The player now holds the skeleton " + heldSkel);
                spcInt.heldSkel = heldSkel;
                spcInt.heldSkel.isGrabbed = true;
                fixedJ.enabled = true;
                fixedJ.connectedBody = heldSkel.transform.parent.GetComponent<Rigidbody2D>();
                spcInt.heldSkel.skelGObject.transform.position = spcInt.skelHeldPos.transform.position;
                animPly.SetBool("isGrabbing", true);
                plyCtrl.jumpHeight = 60;
            }
        }
    }
}
