using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class SpecialInteractions : MonoBehaviour
{
    //Initial code started by Jason on 9/1

    /*
     * TODO:
     * Add cooldown to scratch
     */

    //Public variables and assets
    public Transform player;
    public Transform attackBox;
    public Transform box;
    //public Transform 
    public static Action Climb = delegate { };

    //Private variables
    private bool climb;
    private Vector3 

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
        switch(player.tag)
        {
            case "Blob":
                //Tendril swing
                //Get direction of swingable object
                //Add force in that direction
                break;
            case "Human":
                //Hold box
                //Check if box is in range
                //Grab box (animation stuff, wait for later
                //Attach box
                box.parent = player;
                break;
            case "Cat":
                //Check whether to climb
                if(climb)
                {
                    //Climb
                    Climb();
                }
                else
                {
                    //Scratch
                    //Play animation
                    //Spawn hitbox
                    Instantiate(attackBox, player.position + new Vector3(1, 0, 0), new Quaternion(0, 0, 0, 0), player);
                }
                break;
        }
    }

    //Special for bat is to pick up very light boxes
    private void AirSpecial()
    {
        //Check for a box nearby

        //Check if it is light

        //Attach box to player
        box.parent = player;
    }
}
