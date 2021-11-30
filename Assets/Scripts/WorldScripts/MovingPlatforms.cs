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
    public Rigidbody2D platformRB;
    public float waitTime;
    public float speed;
    public bool moving;
    public bool stopped;
    public int moveTowards;
    public bool waitForPlayer;
    public bool needSignal;

    //Private variables
    private Vector2 currentPos;
    private Vector2 targetPos;
    private Vector2 direction;
    public Vector2 velocity;
    private bool playerOn;
    private bool gotSignal;
    private bool slowToStop;
    private bool stopPointDetection;

    //Action
    public override void Action(bool move)
    {
        if(move)
        {
            gotSignal = false;
        }
        else
        {
            gotSignal = true;
        }
    }

    //Do this on awake
    private void Awake()
    {
        //Set up the next velocity
        currentPos = platform.position;
        targetPos = points[moveTowards].position;
        direction.x = targetPos.x - currentPos.x;
        direction.y = targetPos.y - currentPos.y;
        direction.Normalize();
        velocity = direction * speed;
        //Stop endpoint collision at the beginning
        stopPointDetection = true;
    }

    //FixedUpdate is called once per frame
    void FixedUpdate()
    {
        //If it should be moving do this
        if(moving)
        {
            if (!stopped)
            {
                //Move it
                platformRB.velocity = velocity;
            }
        }
        //Slow to a stop
        if(slowToStop)
        {
            if (!stopped)
            {
                //Check for stop
                //Stop and wait
                stopped = true;
                slowToStop = false;
                //Set up the next velocity
                currentPos = platform.position;
                targetPos = points[moveTowards].position;
                direction.x = targetPos.x - currentPos.x;
                direction.y = targetPos.y - currentPos.y;
                direction.Normalize();
                velocity = direction * speed;
            }
        }
        
    }

    //Check if it has hit an endpoint
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!stopPointDetection)
        {
            if (collision.CompareTag("Endpoint"))
            {
                //Make sure it hits the right one
                if (collision.name == points[moveTowards].name)
                {
                    //Stop and wait
                    slowToStop = true;
                    stopped = true;
                    //Check if it should move
                    if ((needSignal && !gotSignal) || (waitForPlayer && !playerOn))
                    {
                        moving = false;
                    }
                    //Set the next destination up
                    moveTowards++;
                    if (moveTowards >= points.Length)
                    {
                        moveTowards = 0;
                    }
                    //Stop the platform for the wait time
                    platformRB.velocity = Vector2.zero;
                    StartCoroutine("wait");
                }
            }
        }
    }

    //Wait after hitting an end point
    IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);
        stopped = false;
    }

    /*
    //Move stuff with it that is touchng it
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GameAction.PlayerTags(collision.collider.tag))
        {
            collision.transform.SetParent(platform);
        }
    }

    //Don't move stuff with it that isn't touching it
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (GameAction.PlayerTags(collision.collider.tag))
        {
            collision.transform.SetParent(null);
        }
    }
    */
}
