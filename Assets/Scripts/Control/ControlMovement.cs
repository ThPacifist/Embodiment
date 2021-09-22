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
    public Transform player;
    public PlyController plyCntrl;
    public Animator animPly;
    public GameObject target;

    //Private variables
    private string transformTarget = "None";

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        PlyController.Embody += Embody;
    }

    private void OnDisable()
    {
        PlyController.Embody -= Embody;
    }

    //Change transformTarget when entering the triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(transformTarget == "None")
        {
            if (other.tag == "Human" || other.tag == "Cat" || other.tag == "Bat" || other.tag == "Fish")
            {
                transformTarget = "None";
                target = null;
            }
            transformTarget = other.tag;
            target = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == transformTarget)
        {
            transformTarget = "None";
        }
    }

    //When 'r' is pressed change skeletons, movement scripts, and tags to the new skeleton
    private void Embody()
    {
        switch (transformTarget)
        {
            case "Human":
                //Change to human body
                animPly.SetBool("Human", true);
                //Change tag
                player.tag = "Human";
                //Change movement
                plyCntrl.speed = 5;
                plyCntrl.jumpHeight = 5;
                break;
            case "Cat":
                //Change to cat bod
                //animPly.SetBool("Cat", true);
                Debug.Log("Change to Cat");
                //Change tag
                player.tag = "Cat";
                //Change movement
                plyCntrl.speed = 5;
                plyCntrl.jumpHeight = 5;
                break;
            case "Bat":
                //Change to bat body
                animPly.SetBool("Bat", true);
                //Change tag
                player.tag = "Bat";
                //Change movement
                plyCntrl.speed = 5;
                plyCntrl.jumpHeight = 5;
                break;
            case "Fish":
                //Change to fish body
                //animPly.SetBool("Fish", true);
                Debug.Log("Change to Fish");
                //Change tag
                player.tag = "Fish";
                //Change movement
                plyCntrl.speed = 5;
                plyCntrl.jumpHeight = 8;
                break;
            default:
                if(!player.CompareTag("Blob"))
                {
                    //Drop current body
                    animPly.SetBool(player.gameObject.tag, false);
                    //Change tag
                    player.tag = "Blob";
                    //Change movement
                    plyCntrl.speed = 5;
                    plyCntrl.jumpHeight = 8;
                }
                break;
        }
    }
}
