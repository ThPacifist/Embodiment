using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : GameAction
{
    /*
     * Desctiption:
     * This script makes platforms move either on start or when a button is hit
     * Check the moving bool if it should move without input
     * Set the movement speed from 0-1, note that this is a percent of the whole distance, not how many units moved per frame
     * Longer distances mean the number should be smaller
     */

    //Public variables and assets
    public Transform platform;
    public Transform[] points;
    public float speed;
    public bool moving;

    //Private veriables
    private float currentPos;
    private int moveTowards;

    //Action
    public override void Action(bool move)
    {
        if(move)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
    }


    //FixedUpdate is called once per frame
    void FixedUpdate()
    {
        //If it should be moving do this
        if(moving)
        {
            //Calcultate what the current position is
            currentPos += (speed / 100);
            //Lerp it
            platform.position = Vector2.Lerp(points[Mathf.Abs(moveTowards - 1)].position, points[moveTowards].position, currentPos);
            //Find out if it is at the endpoint
            if (currentPos >= 1)
            {
                //Reset endpoint
                currentPos = 0;
                //Set to move towards the endpoint
                moveTowards = Mathf.Abs(moveTowards - 1);
            }
        }    
        
    }
}
