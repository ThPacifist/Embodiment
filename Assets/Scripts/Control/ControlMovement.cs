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
    public CapsuleCollider2D plyCol;
    public Animator animPly;
    public GameObject target;

    //Private variables
    private string transformTarget = "None";
    private bool wait = false;


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
        if(player.tag == "Blob")
        {
            //Change movement
            plyCntrl.speed = 5;
            plyCntrl.jumpHeight = 13;
            plyCol.density = 1;
            //Change Collider
            plyCol.direction = CapsuleDirection2D.Horizontal;
            plyCol.offset = new Vector2(-0.0523f, -0.1200f);
            plyCol.size = new Vector2(1.4153f, 0.9959f);
        }
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
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == transformTarget)
        {
            transformTarget = "None";
            target = null;
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
        if (!wait)
        {
            wait = true;
            if(heldSkeleton != null)
            {
                replaceSkeleton();
            }
            switch (transformTarget)
            {
                case "Human":
                    //Change to human body
                    animPly.SetBool("Human", true);
                    //Change tag
                    player.tag = "Human";
                    //Change movement
                    plyCntrl.speed = 5;
                    plyCntrl.jumpHeight = 35;
                    //Change Collider
                    plyCol.direction = CapsuleDirection2D.Vertical;
                    plyCol.offset = new Vector2(-0.0238f, 0.0030f);
                    plyCol.size = new Vector2(0.9682f, 4.0357f);
                    //Remove skeleton
                    removeSkeleton();
                    break;
                case "Cat":
                    //Change to cat bod
                    animPly.SetBool("Cat", true);
                    //Change tag
                    player.tag = "Cat";
                    //Change movement
                    plyCntrl.speed = 7;
                    plyCntrl.jumpHeight = 40;
                    //Change Collider
                    plyCol.direction = CapsuleDirection2D.Horizontal;
                    plyCol.offset = new Vector2(0.1528f, -0.0274f);
                    plyCol.size = new Vector2(2.2813f, 1.5845f);
                    //Remove skeleton
                    removeSkeleton();
                    break;
                case "Bat":
                    //Change to bat body
                    animPly.SetBool("Bat", true);
                    //Change tag
                    player.tag = "Bat";
                    //Change movement
                    plyCntrl.speed = 5;
                    plyCntrl.jumpHeight = 8;
                    //Change Collider
                    plyCol.direction = CapsuleDirection2D.Vertical;
                    plyCol.offset = new Vector2(0.0047f, -0.0903f);
                    plyCol.size = new Vector2(0.6879f, 1.7104f);
                    //Remove skeleton
                    removeSkeleton();
                    break;
                case "Fish":
                    //Change to fish body
                    //animPly.SetBool("Fish", true);
                    animPly.SetBool("Fish", true);
                    //Change tag
                    player.tag = "Fish";
                    //Change movement
                    plyCntrl.speed = 7;
                    plyCntrl.jumpHeight = 21;
                    plyCol.density = 1.31f;
                    //Change Collider
                    plyCol.direction = CapsuleDirection2D.Horizontal;
                    plyCol.offset = new Vector2(0.0037f, 0.3573f);
                    plyCol.size = new Vector2(3.5232f, 0.8149f);
                    //Remove skeleton
                    removeSkeleton();
                    break;
                default:
                    if (!player.CompareTag("Blob"))
                    {
                        //Drop current body
                        animPly.SetBool(player.gameObject.tag, false);
                        //Change tag
                        player.tag = "Blob";
                        //Change movement
                        plyCntrl.speed = 5;
                        plyCntrl.jumpHeight = 13;
                        plyCol.density = 1;
                        //Change Collider
                        plyCol.direction = CapsuleDirection2D.Horizontal;
                        plyCol.offset = new Vector2(-0.0523f, -0.1200f);
                        plyCol.size = new Vector2(1.4153f, 0.9959f);
                    }
                    break;
            }
            StartCoroutine(waitAFrame());
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
        heldSkeleton.parent = null;
        heldSkeleton.gameObject.SetActive(true);
    }

    //Don't transform for the rest of this frame
    IEnumerator waitAFrame()
    {
        yield return new WaitForEndOfFrame();
        wait = false;
    }
}
