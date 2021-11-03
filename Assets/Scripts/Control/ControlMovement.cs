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
    public bool canEmbody = true;

    //Private variables
    private string transformTarget = "None";
    private bool wait = false;
    AudioManager audioManager;
    ColliderInfo Blob;
    ColliderInfo Human;
    ColliderInfo Cat;
    ColliderInfo Fish;
    ColliderInfo Bat;

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
    }

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    private void Awake()
    {
        Blob.direction = CapsuleDirection2D.Horizontal;
        Blob.offset = new Vector2(0, 0);
        Blob.size = new Vector2(1.830f, 1.366f);

        Human.direction = CapsuleDirection2D.Vertical;
        Human.offset = new Vector2(0, 0);
        Human.size = new Vector2(0.950f, 2.807f);

        Cat.direction = CapsuleDirection2D.Horizontal;
        Cat.offset = new Vector2(0, 0);
        Cat.size = new Vector2(1.5f, 1.5f);

        Bat.direction = CapsuleDirection2D.Vertical;
        Bat.offset = new Vector2(0, 0);
        Bat.size = new Vector2(0.6879f, 1.742f);

        Fish.direction = CapsuleDirection2D.Horizontal;
        Fish.offset = new Vector2(0, 0);
        Fish.size = new Vector2(2.447f, 0.782f);

        Embody(this.tag);
    }

    //Change transformTarget when entering the triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(transformTarget == "None")
        {
            if (other.tag == "Human" || other.tag == "Cat" || other.tag == "Bat" || other.tag == "Fish")
            {
                transformTarget = other.tag;
                target = other.gameObject;
            }
        }

        if(other.CompareTag("Embody"))
        {
            canEmbody = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == transformTarget)
        {
            transformTarget = "None";
            target = null;
        }

        if (other.CompareTag("Embody"))
        {
            canEmbody = true;
        }
    }

    //When 'r' is pressed change skeletons, movement scripts, and tags to the new skeleton
    private void Embody()
    {
        /*
        //Drop current skeleton
        if(heldSkeleton != null)
        {
            heldSkeleton.parent = null;
            heldSkeleton.gameObject.SetActive(true);
            heldSkeleton = null;
        }
        */
        emField.CheckSpace(Vector3.up, Bat);
        if (!wait) //If the allotted time has pass
        {
            if (audioManager != null)
            {
                audioManager.Play("embody", true);
            }
            wait = true;
            if (!plyCntrl.InWater && !spIntr.ObjectHeld && canEmbody)
            {
                if (heldSkeleton != null)
                {
                    replaceSkeleton();
                }
                switch (transformTarget)
                {
                    case "Human":
                        if (emField.CheckSpace(player.position - new Vector3(0, plyCol.bounds.extents.y, 0), Human))//Checks if there is enough space relative to the embody field attached to the player
                        {
                            //Change to human body
                            animPly.SetTrigger("Human");
                            //Change tag
                            player.tag = "Human";
                            //Change movement
                            plyCntrl.speed = 5;
                            plyCntrl.jumpHeight = 27;
                            //Change Collider
                            plyCol.direction = Human.direction;
                            plyCol.offset = Human.offset;
                            plyCol.size = Human.size;
                            //Remove skeleton
                            removeSkeleton();
                        }
                        else
                        {
                            Debug.Log("Not Enough Space");
                        }
                        break;
                    case "Cat":
                        if (emField.CheckSpace(player.position - new Vector3(0, plyCol.bounds.extents.y, 0), Cat))//Checks if there is enough space relative to the embody field attached to the player
                        {
                            //Change to cat bod
                            animPly.SetTrigger("Cat");
                            //Change tag
                            player.tag = "Cat";
                            //Change movement
                            plyCntrl.speed = 7;
                            plyCntrl.jumpHeight = 20;
                            //Change Collider
                            plyCol.direction = Cat.direction;
                            plyCol.offset = Cat.offset;
                            plyCol.size = Cat.size;
                            //Remove skeleton
                            removeSkeleton();
                        }
                        else
                        {
                            Debug.Log("Not Enough Space");
                        }
                        break;
                    case "Bat":
                        if (emField.CheckSpace(player.position - new Vector3(0, plyCol.bounds.extents.y, 0), Bat))//Checks if there is enough space relative to the embody field attached to the player
                        {
                            //Change to bat body
                            animPly.SetTrigger("Bat");
                            //Change tag
                            player.tag = "Bat";
                            //Change movement
                            plyCntrl.speed = 5;
                            plyCntrl.jumpHeight = 10;
                            //Change Collider
                            plyCol.direction = Bat.direction;
                            plyCol.offset = Bat.offset;
                            plyCol.size = Bat.size;
                            //Remove skeleton
                            removeSkeleton();
                        }
                        else
                        {
                            Debug.Log("Not Enough Space");
                        }
                        break;
                    case "Fish":
                        if (emField.CheckSpace(player.position - new Vector3(0, plyCol.bounds.extents.y, 0), Fish))//Checks if there is enough space relative to the embody field attached to the player
                        {
                            //Change to fish body
                            //animPly.SetBool("Fish", true);
                            animPly.SetTrigger("Fish");
                            //Change tag
                            player.tag = "Fish";
                            //Change movement
                            plyCntrl.speed = 7;
                            plyCntrl.jumpHeight = 13.8f;
                            plyCol.density = 1.31f;
                            //Change Collider
                            plyCol.direction = Fish.direction;
                            plyCol.offset = Fish.offset;
                            plyCol.size = Fish.size;
                            //Remove skeleton
                            removeSkeleton();
                        }
                        else
                        {
                            Debug.Log("Not Enough Space");
                        }
                        break;
                    default://Unembodies the player from its current skeleton
                        if (!player.CompareTag("Blob"))
                        {
                            //Drop current body
                            animPly.SetTrigger("Blob");
                            //Change tag
                            player.tag = "Blob";
                            //Change movement
                            plyCntrl.speed = 5;
                            plyCntrl.jumpHeight = 18.1f;
                            plyCol.density = 1;
                            //Change Collider
                            plyCol.direction = Blob.direction;
                            plyCol.offset = Blob.offset;
                            plyCol.size = Blob.size;
                        }
                        break;
                }
            }
            else if (plyCntrl.InWater)
            {
                //indication of unemboding goes here
                Debug.Log("Cannot Unembody in Water");
            }
            else if (spIntr.ObjectHeld)
            {
                Debug.Log("Cannot Unembody while holding a box");
            }
            StartCoroutine(waitAFrame());
        }
    }
    
    //Use this function only for debugging, Change inputted string at start to start in different
    
    //IMPORTANT:    Make sure to change string to Blob when finished
    private void Embody(string form)
    {
        switch (form)
        {
            case "Human":
                if (emField.CheckSpace(player.position - new Vector3(0, plyCol.bounds.extents.y, 0), Human))//Checks if there is enough space
                {
                    //Change to human body
                    animPly.SetBool("Human", true);
                    //Change tag
                    player.tag = "Human";
                    //Change movement
                    plyCntrl.speed = 5;
                    plyCntrl.jumpHeight = 27;
                    //Change Collider
                    plyCol.direction = Human.direction;
                    plyCol.offset = Human.offset;
                    plyCol.size = Human.size;
                    //Remove skeleton
                    removeSkeleton();
                }
                else
                {
                    Debug.Log("Not Enough Space");
                }
                break;
            case "Cat":
                if (emField.CheckSpace(player.position - new Vector3(0, plyCol.bounds.extents.y, 0), Cat))//Checks if there is enough space
                {
                    //Change to cat bod
                    animPly.SetBool("Cat", true);
                    //Change tag
                    player.tag = "Cat";
                    //Change movement
                    plyCntrl.speed = 7;
                    plyCntrl.jumpHeight = 32;
                    //Change Collider
                    plyCol.direction = Cat.direction;
                    plyCol.offset = Cat.offset;
                    plyCol.size = Cat.size;
                    //Remove skeleton
                    removeSkeleton();
                }
                else
                {
                    Debug.Log("Not Enough Space");
                }
                break;
            case "Bat":
                if (emField.CheckSpace(player.position - new Vector3(0, plyCol.bounds.extents.y, 0), Bat))//Checks if there is enough space
                {
                    //Change to bat body
                    animPly.SetBool("Bat", true);
                    //Change tag
                    player.tag = "Bat";
                    //Change movement
                    plyCntrl.speed = 5;
                    plyCntrl.jumpHeight = 10;
                    //Change Collider
                    plyCol.direction = Bat.direction;
                    plyCol.offset = Bat.offset;
                    plyCol.size = Bat.size;
                    //Remove skeleton
                    removeSkeleton();
                }
                else
                {
                    Debug.Log("Not Enough Space");
                }
                break;
            case "Fish":
                if (emField.CheckSpace(player.position - new Vector3(0, plyCol.bounds.extents.y, 0), Fish))//Checks if there is enough space
                {
                    //Change to fish body
                    //animPly.SetBool("Fish", true);
                    animPly.SetBool("Fish", true);
                    //Change tag
                    player.tag = "Fish";
                    //Change movement
                    plyCntrl.speed = 7;
                    plyCntrl.jumpHeight = 13.8f;
                    plyCol.density = 1.31f;
                    //Change Collider
                    plyCol.direction = Fish.direction;
                    plyCol.offset = Fish.offset;
                    plyCol.size = Fish.size;
                    //Remove skeleton
                    removeSkeleton();
                }
                else
                {
                    Debug.Log("Not Enough Space");
                }
                break;
            default://Unembodies the player from its current skeleton
                if (player.CompareTag("Blob"))
                {
                    //Drop current body
                    animPly.SetBool(player.gameObject.tag, false);
                    //Change tag
                    player.tag = "Blob";
                    //Change movement
                    plyCntrl.speed = 5;
                    plyCntrl.jumpHeight = 18.1f;
                    plyCol.density = 1;
                    //Change Collider
                    plyCol.direction = Blob.direction;
                    plyCol.offset = Blob.offset;
                    plyCol.size = Blob.size;
                }
                break;
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
    private void replaceSkeleton()
    {

        Vector3 curPos = new Vector3(player.position.x, player.position.y + 1, 0);
        heldSkeleton.position = curPos;
        heldSkeleton.parent = null;
        heldSkeleton.gameObject.SetActive(true);
        heldSkeleton = null;
        transformTarget = "None";
    }

    //Don't transform for the rest of this frame
    IEnumerator waitAFrame()
    {
        yield return new WaitForEndOfFrame();
        wait = false;
    }
    //Used For ShriekerField script to remove skeleton from player
    public void DestorySkeleton()
    {
        transformTarget = "None";
        Embody();
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

    //player.position - new Vector2(0, plyCol.bounds.extents.y, 0), 
}
