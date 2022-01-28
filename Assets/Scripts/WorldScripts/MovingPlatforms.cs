using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : GameAction
{
    /*
     * Desctiption:
     * This script makes platforms move either on start or when a button is hit
     * Check the moving bool if it should move without input
     * Move time is the time it will take to go between any two points, the speed need not be consistant if there are more than 2 points
     * Longer distances mean the number should be smaller
     */

    //Public variables and assets
    public float waitTime;
    public float moveTime;
    public int waitPos;
    public bool needSignal;
    public bool waitForPlayer;
    public bool consistantSpeed;
    public Transform platform;
    public Transform[] points;
    public float speed;
    public int moveTowards = 1;
    public int currentPos = 0;
    public bool stopped;
    public bool moving;

    //Private variables
    private bool gotSignal;
    private bool playerOn;


    //Action from a button/lever
    public override void Action()
    {
        //Invert whether it has a signal
        gotSignal = !gotSignal;
    }

    public override void Action(bool holdPos)
    {
        //If hold is true, set got signal to false, else set it to true
        if (holdPos)
        {
            gotSignal = false;
        }
        else
        {
            gotSignal = true;
        }
    }

    //Runs at start
    private void Start()
    {
        //Check if it should move by default
        if(needSignal || waitForPlayer)
        {
            moving = false;
        }
        //Calculate speed
        speed = Vector3.Distance(points[0].position, points[1].position);
        speed /= moveTime * 60;

        Debug.Log("Start: " + moving);
    }

    //Fixed Update is called 60 times  second
    private void FixedUpdate()
    {
        //If it is moving, move
        if (moving)
        {
            //Move the platform to the next point
            platform.position = Vector3.MoveTowards(platform.position, points[moveTowards].position, speed);
            //Check if it has arrived
            if(platform.position == points[moveTowards].position)
            {
                //Change the target
                changeTarget();

                //Check if it should restart
                //If it needs theplayer, call that method
                if (waitForPlayer)
                {
                    pCheck();
                }
                //If it needs a signal, call that method
                if (needSignal && moving)
                {
                    nsCheck();
                }
                
                if (moving)
                {
                    stopped = true;
                    StartCoroutine("wait");
                }
            }
        }
        //Else, check why it isn't
        else if(!stopped)
        {
            //If it needs the player, call that method
            if(waitForPlayer)
            {
                pCheck();
            }
            //If it needs a signal, call that method
            if(needSignal)
            {
                if (waitForPlayer)
                {
                    if(moving)
                    {
                        nsCheck();
                    }
                }
                else
                {
                    nsCheck();
                }
            }
        }
    }

    //Player check
    private void pCheck()
    {
        Debug.Log("Pcheck: " + moving);
        //Check if it should stop here
        if (!playerOn &&  (currentPos == waitPos))
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
    }

    //Need signal check
    private void nsCheck()
    {
        Debug.Log("NScheck: " + moving);
        if (!gotSignal && (currentPos == waitPos))
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
    }

    //Change target position
    private void changeTarget()
    {
        //Set the current position
        currentPos = moveTowards;

        //Increment target position
        moveTowards++;

        //Wrap around to 0 if needed
        if(moveTowards >= points.Length)
        {
            moveTowards = 0;
        }

        //Recalculate speed if needed
        if (!consistantSpeed)
        {
            speed = Vector3.Distance(points[currentPos].position, points[moveTowards].position);
            speed /= moveTime * 60;
        }
    }

    //Wait after hitting an end point
    IEnumerator wait()
    {
        moving = false;
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
