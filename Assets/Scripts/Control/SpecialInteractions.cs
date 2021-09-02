using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class SpecialInteractions : MonoBehaviour
{
    //Initial code started by Jason on 9/1
    //Picking up and dropping boxes done on by Jason 9/2
    /*
     * TODO:
     * Add cooldown to scratch
     */

        //IMPORTANT: For any successfully executed special action set a cooldown time, unset special ready, and call the cooldown timer!!!

    //Public variables and assets
    public Transform player;
    public Transform attackBox;
    public static Action Climb = delegate { };
    public static Action<Transform> SelectBox = delegate { };

    //Private variables
    private bool climb;
    private bool canHold;
    private bool objectHeld;
    private bool specialReady = true;
    private float cooldownTime;
    private Transform box;
    private Transform heldBox;

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        WaterMovement.Special += WaterSpecial;
        LandMovement.Special += LandSpecial;
        AirMovement.Special += AirSpecial;
    }

    private void OnDisable()
    {
        WaterMovement.Special -= WaterSpecial;
        LandMovement.Special -= LandSpecial;
        AirMovement.Special -= AirSpecial;
    }

    //Special for fish is going to be written by Benathen
    private void WaterSpecial()
    {

    }

    //Special for blob is tendril grab and swing
    //Special for cat is climb or cat scratch
    //Special for human is to pick up boxes up to medium weight
    private void LandSpecial()
    {
        if (specialReady)
        {
            switch (player.tag)
            {
                case "Blob":
                    //Tendril swing
                    //Get direction of swingable object
                    //Add force in that direction
                    break;
                case "Human":
                    //Check if there is a box to hold or a box being held
                    if (canHold && !objectHeld)
                    {
                        //Attach box
                        box.parent = player;
                        heldBox = box;
                        objectHeld = true;
                        //Cooldown
                        cooldownTime = 1;
                        specialReady = false;
                        StartCoroutine("SpecialCoolDown");
                    }
                    else if (objectHeld)
                    {
                        //Drop box
                        heldBox.parent = null;
                        objectHeld = false;
                        //Cooldown
                        cooldownTime = 1;
                        specialReady = false;
                        StartCoroutine("SpecialCoolDown");
                    }
                    break;
                case "Cat":
                    //Check whether to climb
                    if (climb)
                    {
                        //Climb
                        Climb();
                        //Cooldown
                        cooldownTime = 1;
                        specialReady = false;
                        StartCoroutine("SpecialCoolDown");
                    }
                    else
                    {
                        //Spawn hitbox
                        Instantiate(attackBox, player.position + new Vector3(1, 0, 0), new Quaternion(0, 0, 0, 0), player);
                        //Cooldown
                        cooldownTime = 1;
                        specialReady = false;
                        StartCoroutine("SpecialCoolDown");
                    }
                    break;
            }
        }
    }

    //Special for bat is to pick up very light boxes
    private void AirSpecial()
    {
        if (specialReady)
        {
            if (canHold && !objectHeld)
            {
                //Attach box
                box.parent = player;
                heldBox = box;
                objectHeld = true;
                cooldownTime = 1;
                specialReady = false;
                StartCoroutine("SpecialCoolDown");
            }
            else if (objectHeld)
            {
                //Drop box
                Debug.Log("Drop box");
                heldBox.parent = null;
                objectHeld = false;
                cooldownTime = 1;
                specialReady = false;
                StartCoroutine("SpecialCoolDown");
            }
        }
    }

    //Check for boxes
    private void OnTriggerEnter(Collider other)
    {
        //Check if it is a box
        if(other.CompareTag("LBox") || other.CompareTag("MBox") || other.CompareTag("HBox") && !objectHeld)
        {
            //See if the human can lift it
            if (player.CompareTag("Human") && (other.CompareTag("LBox") || other.CompareTag("MBox")))
            { 
                box = other.transform;
                SelectBox(other.transform);
                canHold = true;
            }
            //See if the bat can lift it
            else if (player.CompareTag("Bat") && (other.CompareTag("LBox")))
            {
                box = other.transform;
                SelectBox(other.transform);
                canHold = true;
            }
            //See if the human can push it
            else if(player.CompareTag("Human") && (other.CompareTag("LBox") || other.CompareTag("MBox") || other.CompareTag("HBox")))
            {
                SelectBox(other.transform);
                canHold = true;
            }
            //See if anyone can push it
            else if(other.CompareTag("LBox") || other.CompareTag("MBox"))
            {
                SelectBox(other.transform);
                canHold = true;
            }
        }
    }

    //Remove boxes from selection
    private void OnTriggerExit(Collider other)
    {
        box = null;
        SelectBox(null);
        canHold = false;
    }

    //Special cooldown
    IEnumerator SpecialCoolDown()
    {
        yield return new WaitForSeconds(cooldownTime);
        specialReady = true;
    }
}
