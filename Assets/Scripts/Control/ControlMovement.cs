using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;


 struct ColliderInfo
{
    public Vector2 size;
    public Vector2 offset;
    public CapsuleDirection2D direction;
}

public class ControlMovement : MonoBehaviour
{
    //Started on 8/27 by Jason
    //Initial code finished on 8/30 by Jason
    /*
     * TODO:
     * Change models with the change
     */
    //Assets and Public Variables
    public Transform heldSkeleton;
    public Transform player;
    public PlyController plyCntrl;
    public PlayerData playerData;
    public SpecialInteractions spIntr;
    public CapsuleCollider2D plyCol;
    public Animator animPly;
    public SkeletonTrigger skeleton;
    public static bool canEmbody = true;
    public static bool canDisembody = false;

    //Private variables
    AudioManager audioManager;

    [SerializeField]
    EmbodyField emField;

    [HideInInspector]
    public SkeletonTrigger skeloData;

    //Default Values for the blob
    [HideInInspector]
    public string defaultName = "Blob";
    [HideInInspector]
    public float defaultSpeed = 5;
    [HideInInspector]
    public float defaultJumpHeight = 18.1f;
    [HideInInspector]
    public Vector2 defaultSize = new Vector2(1.830f, 1.366f);
    [HideInInspector]
    public Vector2 defaultOffset = new Vector2(0, 0);
    [HideInInspector]
    public CapsuleDirection2D defaultDirection = CapsuleDirection2D.Horizontal;
    [HideInInspector]
    public float defaultDensity = 1;
    [SerializeField]
    public RuntimeAnimatorController defaultController;

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        PlyController.Embody += Embody;
    }

    private void OnDisable()
    {
        PlyController.Embody -= Embody;
        PlyController.Embody -= Disembody;
    }

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    private void Awake()
    {
        //Embody(this.tag);
    }

    
    //When "r" is pressed, the player embodies the skeleton
    void Embody()
    {
        //If player is not embodying a skeleton, embody the skeleton
        if(skeleton != null)
        {
            if (emField.CheckSpace(player.position - new Vector3(0, plyCol.bounds.extents.y, 0), skeleton) && canEmbody 
                && plyCntrl.isGrounded())
            {
                if (audioManager != null)
                {
                    audioManager.Play("embody");
                }

                //Changes players values to be the skeleton
                spIntr.PickUpSkeleton(null);
                player.tag = skeleton.Name;
                plyCntrl.speed = skeleton.speed;
                plyCntrl.jumpHeight = skeleton.jumpHeight;
                plyCol.size = skeleton.colliderSize;
                plyCol.offset = skeleton.colliderOffset;
                plyCol.direction = skeleton.direction;
                plyCol.density = skeleton.density;
                skeloData = skeleton;

                //Changes players sprite to be the skeleton
                if(skeleton.controller != null)
                    animPly.runtimeAnimatorController = skeleton.controller;
                animPly.SetTrigger(skeleton.Name);
                animPly.SetInteger("Form", skeleton.Form);

                //Attach skeleton to player and disable it
                heldSkeleton = skeleton.transform.parent;
                heldSkeleton.parent = player;
                heldSkeleton.gameObject.SetActive(false);

                //Unsubscribes embody funciton and subscribes disembody funciton
                PlyController.Embody -= Embody;
                PlyController.Embody += Disembody;
                canDisembody = true;
            }
            else if(!emField.CheckSpace(player.position - new Vector3(0, plyCol.bounds.extents.y, 0), skeleton))
            {
                Debug.Log("There is not enough space");
            }
            else if(!canEmbody)
            {
                Debug.Log("Cannot Embody for some reason");
            }
        }
        else
        {
            Debug.Log("There is no skeleton");
        }
    }
    //Disembodies the player
    void Disembody()
    {
        if (heldSkeleton != null)
        {
            if (!plyCntrl.InWater && !spIntr.objectHeld && canDisembody && plyCntrl.isGrounded())
            {
                if (audioManager != null)
                {
                    audioManager.Play("embody");
                }

                //Changes players values to be the blob
                player.tag = defaultName;
                plyCntrl.speed = defaultSpeed;
                plyCntrl.jumpHeight = defaultJumpHeight;
                plyCol.size = defaultSize;
                plyCol.offset = defaultOffset;
                plyCol.direction = defaultDirection;
                plyCol.density = defaultDensity;
                skeleton = null;
                skeloData = null;

                //Changes players sprite to be the blob
                //animPly.runtimeAnimatorController = defaultController;
                animPly.SetTrigger("Disembody");
                animPly.SetInteger("Form", 0);

                //Renables skeleton
                heldSkeleton.gameObject.SetActive(true);
                heldSkeleton.parent = null;
                spIntr.PickUpSkeleton(heldSkeleton.GetComponentInChildren<SkeletonTrigger>());
                skeleton = heldSkeleton.GetComponentInChildren<SkeletonTrigger>();
                heldSkeleton = null;

                //Unsubscribes disembody funciton and subscribes embody funciton
                PlyController.Embody -= Disembody;
                PlyController.Embody += Embody;
                canEmbody = true;
            }
            else if(plyCntrl.InWater)
            {
                Debug.Log("Cannot disembody in water");
            }
            else if(spIntr.objectHeld)
            {
                Debug.Log("Cannot disembody while carrying an object");
            }
        }
        else
        {
            Debug.Log("Held skeleton is null");
        }
    }

    //Used For ShriekerField script to remove skeleton from player
    public void DestorySkeleton()
    {
        if (audioManager != null && heldSkeleton != null)
        {
            audioManager.Play("embody");
        }

        //Makes it so player is no longer holding the skeleton
        spIntr.PickUpSkeleton(null);

        //Changes players values to be the blob
        player.tag = "Blob";
        plyCntrl.speed = 5;
        plyCntrl.jumpHeight = 18.1f;
        plyCol.size = new Vector2(1.830f, 1.366f);
        plyCol.offset = new Vector2(0, 0);
        plyCol.direction = CapsuleDirection2D.Horizontal;
        plyCol.density = 1;

        //Changes players sprite to be the blob
        animPly.SetTrigger("Blob");
        animPly.SetInteger("Form", 0);

        //Move skeleton back to respawn position
        if (skeleton != null)
        skeleton.skeloScript.RespawnSkeleton();
        skeleton = null;

        //Activates skeleton and finishes disconnecting it from the player
        if (heldSkeleton != null)
        {
            heldSkeleton.parent = null;
            heldSkeleton.gameObject.SetActive(true);
            heldSkeleton = null;
        }

        //Unsubscribes disembody funciton and subscribes embody funciton
        PlyController.Embody -= Disembody;
        PlyController.Embody += Embody;
        canEmbody = true;
    }

    //Set values of next skeleton to new values
    public void SetEmbodyValues(SkeletonTrigger skelo)
    {
        skeleton = skelo;
    }

    void ChangeForm(ColliderInfo from, ColliderInfo to)
    {

    }

    private void OnDrawGizmos()
    {
        //Area Player occupies in Grid
        Vector3Int topLeft = Vector3Int.FloorToInt(new Vector3(plyCol.bounds.min.x, plyCol.bounds.max.y+1, 0));
        Vector3Int topRight = Vector3Int.FloorToInt(plyCol.bounds.max + new Vector3(1, 1, 0));
        Vector3Int botRight = Vector3Int.FloorToInt(new Vector3 (plyCol.bounds.max.x+1, plyCol.bounds.min.y, 0));
        Vector3Int botLeft = Vector3Int.FloorToInt(plyCol.bounds.min);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(topRight, botRight);
        Gizmos.DrawLine(botRight, botLeft);
        Gizmos.DrawLine(botLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
    }
}
