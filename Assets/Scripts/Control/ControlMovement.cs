using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    [SerializeField]
    EmbodyField emField;

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
                        if (emField.CheckSpace())//Checks if there is enough space relative to the embody field attached to the player
                        {
                            //Change to human body
                            animPly.SetTrigger("Human");
                            //Change tag
                            player.tag = "Human";
                            //Change movement
                            plyCntrl.speed = 5;
                            plyCntrl.jumpHeight = 27;
                            //Change Collider
                            plyCol.direction = CapsuleDirection2D.Vertical;
                            plyCol.offset = new Vector2(0, 0);
                            plyCol.size = new Vector2(0.950f, 3.380f);
                            //Remove skeleton
                            removeSkeleton();
                        }
                        else
                        {
                            Debug.Log("Not Enough Space");
                        }
                        break;
                    case "Cat":
                        if (emField.CheckSpace())//Checks if there is enough space relative to the embody field attached to the player
                        {
                            //Change to cat bod
                            animPly.SetTrigger("Cat");
                            //Change tag
                            player.tag = "Cat";
                            //Change movement
                            plyCntrl.speed = 7;
                            plyCntrl.jumpHeight = 20;
                            //Change Collider
                            plyCol.direction = CapsuleDirection2D.Horizontal;
                            plyCol.offset = new Vector2(0, 0);
                            plyCol.size = new Vector2(1.5f, 1.5f);
                            //Remove skeleton
                            removeSkeleton();
                        }
                        else
                        {
                            Debug.Log("Not Enough Space");
                        }
                        break;
                    case "Bat":
                        if (emField.CheckSpace())//Checks if there is enough space relative to the embody field attached to the player
                        {
                            //Change to bat body
                            animPly.SetTrigger("Bat");
                            //Change tag
                            player.tag = "Bat";
                            //Change movement
                            plyCntrl.speed = 5;
                            plyCntrl.jumpHeight = 10;
                            //Change Collider
                            plyCol.direction = CapsuleDirection2D.Vertical;
                            plyCol.offset = new Vector2(0, 0);
                            plyCol.size = new Vector2(0.6879f, 1.742f);
                            //Remove skeleton
                            removeSkeleton();
                        }
                        else
                        {
                            Debug.Log("Not Enough Space");
                        }
                        break;
                    case "Fish":
                        if (emField.CheckSpace())//Checks if there is enough space relative to the embody field attached to the player
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
                            plyCol.direction = CapsuleDirection2D.Horizontal;
                            plyCol.offset = new Vector2(0, 0);
                            plyCol.size = new Vector2(2.447f, 0.782f);
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
                            plyCol.direction = CapsuleDirection2D.Horizontal;
                            plyCol.offset = new Vector2(6.838e-09f, 0.057f);
                            plyCol.size = new Vector2(1.830f, 1.366f);
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
                if (emField.CheckSpace())//Checks if there is enough space
                {
                    //Change to human body
                    animPly.SetBool("Human", true);
                    //Change tag
                    player.tag = "Human";
                    //Change movement
                    plyCntrl.speed = 5;
                    plyCntrl.jumpHeight = 27;
                    //Change Collider
                    plyCol.direction = CapsuleDirection2D.Vertical;
                    plyCol.offset = new Vector2(0, 0);
                    plyCol.size = new Vector2(0.950f, 3.380f);
                    //Remove skeleton
                    removeSkeleton();
                }
                else
                {
                    Debug.Log("Not Enough Space");
                }
                break;
            case "Cat":
                if (emField.CheckSpace())//Checks if there is enough space
                {
                    //Change to cat bod
                    animPly.SetBool("Cat", true);
                    //Change tag
                    player.tag = "Cat";
                    //Change movement
                    plyCntrl.speed = 7;
                    plyCntrl.jumpHeight = 32;
                    //Change Collider
                    plyCol.direction = CapsuleDirection2D.Horizontal;
                    plyCol.offset = new Vector2(0, 0);
                    plyCol.size = new Vector2(1.5f, 1.5f);
                    //Remove skeleton
                    removeSkeleton();
                }
                else
                {
                    Debug.Log("Not Enough Space");
                }
                break;
            case "Bat":
                if (emField.CheckSpace())//Checks if there is enough space
                {
                    //Change to bat body
                    animPly.SetBool("Bat", true);
                    //Change tag
                    player.tag = "Bat";
                    //Change movement
                    plyCntrl.speed = 5;
                    plyCntrl.jumpHeight = 10;
                    //Change Collider
                    plyCol.direction = CapsuleDirection2D.Vertical;
                    plyCol.offset = new Vector2(0, 0);
                    plyCol.size = new Vector2(0.6879f, 1.742f);
                    //Remove skeleton
                    removeSkeleton();
                }
                else
                {
                    Debug.Log("Not Enough Space");
                }
                break;
            case "Fish":
                if (emField.CheckSpace())//Checks if there is enough space
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
                    plyCol.direction = CapsuleDirection2D.Horizontal;
                    plyCol.offset = new Vector2(0, 0);
                    plyCol.size = new Vector2(2.447f, 0.782f);
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
                    plyCol.direction = CapsuleDirection2D.Horizontal;
                    plyCol.offset = new Vector2(6.838e-09f, 0.057f);
                    plyCol.size = new Vector2(1.830f, 1.366f);
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
        int x;
        if(PlyController.Right)
        {
            x = 1;
        }
        else
        {
            x = -1;
        }

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
