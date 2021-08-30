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
     * Add script to change bodies
     */
    //Assets and Public Variables
    public Transform player;

    //Private variables
    private string transformTarget = "None";

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        WaterMovement.Embody += Embody;
        AirMovement.Embody += Embody;
        LandMovement.Embody += Embody;
    }

    private void OnDisable()
    {
        WaterMovement.Embody -= Embody;
        AirMovement.Embody -= Embody;
        LandMovement.Embody -= Embody;
    }

    //Change transformTarget when entering the triggers
    private void OnTriggerEnter(Collider other)
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

    private void OnTriggerExit(Collider other)
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
                //Change tag
                player.tag = "Human";
                //Change movement script
                player.GetComponent<AirMovement>().enabled = false;
                player.GetComponent<WaterMovement>().enabled = false;
                player.GetComponent<LandMovement>().enabled = true;
                break;
            case "Cat":
                //Change to cat body
                //Change tag
                player.tag = "Cat";
                //Change movement script
                player.GetComponent<AirMovement>().enabled = false;
                player.GetComponent<WaterMovement>().enabled = false;
                player.GetComponent<LandMovement>().enabled = true;
                break;
            case "Bat":
                //Change to fish body
                //Change tag
                player.tag = "Bat";
                //Change movement script
                player.GetComponent<AirMovement>().enabled = true;
                player.GetComponent<WaterMovement>().enabled = false;
                player.GetComponent<LandMovement>().enabled = false;
                break;
            case "Fish":
                //Change to fish body
                //Change tag
                player.tag = "Fish";
                //Change movement script
                player.GetComponent<AirMovement>().enabled = false;
                player.GetComponent<WaterMovement>().enabled = true;
                player.GetComponent<LandMovement>().enabled = false;
                break;
            default:
                if(!player.CompareTag("Blob"))
                {
                    //Drop current body
                    //Change tag
                    player.tag = "Blob";
                    //Change movement script
                    player.GetComponent<AirMovement>().enabled = false;
                    player.GetComponent<WaterMovement>().enabled = false;
                    player.GetComponent<LandMovement>().enabled = true;
                }
                break;
        }
    }
}
