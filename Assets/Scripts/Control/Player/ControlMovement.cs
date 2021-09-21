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
    public MeshFilter playerModel;
    public Mesh blobModel;
    public Mesh humanModel;
    public Mesh batModel;
    public Mesh fishModel;
    public Mesh catModel;
    public PlyController plyCntrl;

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
            }
            transformTarget = other.tag;
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
                playerModel.sharedMesh = humanModel;
                //Change tag
                player.tag = "Human";
                //Change movement
                plyCntrl.speed = 5;
                plyCntrl.jumpHeight = 5;
                break;
            case "Cat":
                //Change to cat body
                playerModel.sharedMesh = catModel;
                //Change tag
                player.tag = "Cat";
                //Change movement
                plyCntrl.speed = 5;
                plyCntrl.jumpHeight = 5;
                break;
            case "Bat":
                //Change to fish body
                playerModel.sharedMesh = batModel;
                //Change tag
                player.tag = "Bat";
                //Change movement
                plyCntrl.speed = 5;
                plyCntrl.jumpHeight = 5;
                break;
            case "Fish":
                //Change to fish body
                playerModel.sharedMesh = fishModel;
                //Change tag
                player.tag = "Fish";
                //Change movement
                plyCntrl.speed = 5;
                plyCntrl.jumpHeight = 5;
                break;
            default:
                if(!player.CompareTag("Blob"))
                {
                    //Drop current body
                    playerModel.sharedMesh = blobModel;
                    //Change tag
                    player.tag = "Blob";
                    //Change movement
                    plyCntrl.speed = 5;
                    plyCntrl.jumpHeight = 5;
                }
                break;
        }
    }
}
