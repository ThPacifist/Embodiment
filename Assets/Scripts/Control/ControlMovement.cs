using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;


public struct ColliderInfo
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
    public SpecialInteractions spIntr;
    public CapsuleCollider2D plyCol;
    public Animator animPly;
    public GameObject target;
    public SkeletonTrigger skeleton;
    public static bool canEmbody = true;
    public static bool canDisembody = false;

    //Private variables
    AudioManager audioManager;

    [SerializeField]
    EmbodyField emField;
    [SerializeField]
    Tilemap tileMap;


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
            if (emField.CheckSpace(player.position - new Vector3(0, plyCol.bounds.extents.y, 0), skeleton) && canEmbody)
            {
                if (audioManager != null)
                {
                    audioManager.Play("embody", true);
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

                //Changes players sprite to be the skeleton
                animPly.SetTrigger(skeleton.Name);

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
            if (!plyCntrl.InWater && !spIntr.objectHeld && canDisembody)
            {
                if (audioManager != null)
                {
                    audioManager.Play("embody", true);
                }

                //Changes players values to be the blob
                player.tag = "Blob";
                plyCntrl.speed = 5;
                plyCntrl.jumpHeight = 18.1f;
                plyCol.size = new Vector2(1.830f, 1.366f);
                plyCol.offset = new Vector2(0, 0);
                plyCol.direction = CapsuleDirection2D.Horizontal;
                plyCol.density = 1;
                skeleton = null;

                //Changes players sprite to be the blob
                animPly.SetTrigger("Disembody");

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
            Debug.Log("Something went wrong in Control Movement");
        }
    }

    //Remove skeleton
    private void removeSkeleton()
    {
        heldSkeleton = target.transform;
        heldSkeleton.parent = player;
        heldSkeleton.gameObject.SetActive(false);
    }

    //Replace skeleton
    private void SpawnSkeleton()
    {
        /*Vector3 curPos = new Vector3(player.position.x, player.position.y + 1, 0);
        heldSkeleton.position = curPos;*/
        heldSkeleton.parent = null;
        heldSkeleton.gameObject.SetActive(true);
        heldSkeleton = null;
    }

    //Used For ShriekerField script to remove skeleton from player
    public void DestorySkeleton()
    {
        if (audioManager != null && heldSkeleton != null)
        {
            audioManager.Play("embody", true);
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

        //Move skeleton back to respawn position
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

    Vector2 PlaceSkeleton()
    {
        Vector2Int tR = Vector2Int.FloorToInt(plyCol.bounds.max + Vector3.one);
        Vector2Int bL = Vector2Int.FloorToInt(plyCol.bounds.min + -Vector3.one);

        for(int i = bL.x; i < tR.x; i++)
        {
            for(int j = bL.y; j < tR.y; j++)
            {
                if(!tileMap.GetTile(new Vector3Int(i, j, 0)))
                {
                    return new Vector2(i + 0.5f, 0.5f);
                }
            }
        }
        Debug.Log("Something has gone wrong in Control Movement");
        return player.position;
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
