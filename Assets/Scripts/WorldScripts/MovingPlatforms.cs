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
    public float waitTime;
    public float speed;
    public bool moving;
    public bool stopped;
    public float currentPos;
    public int moveTowards;
    public bool needSignal;
    public bool waitForPlayer;
    
    //Private variables
    private bool gotSignal;
    private bool playerOn;

    //Action
    public override void Action()
    {
        gotSignal = !gotSignal;
    }

    public override void Action(bool move)
    {
        if (move)
        {
            gotSignal = false;
        }
        else
        {
            gotSignal = true;
        }
    }

    //FixedUpdate is called once per frame
    void FixedUpdate()
    {
        //If it should be moving do this

        if (!stopped && moving)
        {
            //Move it
            platform.position = Vector3.MoveTowards(platform.position, points[moveTowards].transform.position, speed * Time.deltaTime);
            //Check to see if it has hit the endpoint
            if (platform.position == points[moveTowards].transform.position)
            {
                //Change movement target
                moveTowards++;
                if (moveTowards >= points.Length)
                {
                    moveTowards = 0;
                }
                //WStop
                stopped = true;
                //Make check if it should move
                if ((!gotSignal && needSignal) || (!playerOn && waitForPlayer))
                {
                    moving = false;
                }
                else
                {
                    //Wait a little
                    StartCoroutine("wait");
                }
            }
        }
        else if(!moving)
        {
            if ((gotSignal && needSignal) && !waitForPlayer)
            {
                StartCoroutine("wait");
            }
            else if((playerOn && waitForPlayer) && !needSignal)
            {
                StartCoroutine("wait");
            }
            else if((gotSignal && needSignal) && (playerOn && waitForPlayer))
            {
                StartCoroutine("wait");
            }
        }

    }

    //Wait after hitting an end point
    IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);
        stopped = false;
        moving = true;
    }

    //Move stuff with it that is touchng it
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Parent to platform
        collision.transform.SetParent(platform);
        //Check if it was the player
        if (GameAction.PlayerTags(collision.collider.tag))
        {
            //Set playerOn
            playerOn = true;
        }
    }

    //Don't move stuff with it that isn't touching it
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Remove parent
        collision.transform.SetParent(null);
        //Check if it was the player
        if (GameAction.PlayerTags(collision.collider.tag))
        {
            //Set playerOn
            playerOn = false;
        }
    }
}
