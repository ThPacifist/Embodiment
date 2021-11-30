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
    private Vector2 velocity;
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
        StartCoroutine("pointWait");
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
            //Slow down
            //velocity *= 0.5f;
            //Check for stop
            if(velocity.x + velocity.y < 0.5)
            {
                //Stop and wait
                stopped = true;
                slowToStop = false;
                StartCoroutine("wait");
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
        Debug.Log("I'm in a trigger");
        if (!stopPointDetection)
        {
            if (collision.CompareTag("Endpoint"))
            {
                Debug.Log("Hit endpoint");
                stopPointDetection = true;
                StartCoroutine("pointWait");
                StartCoroutine("wait");
                //Stop and wait
                slowToStop = true;
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
            }
        }
    }

    //Wait after hitting an end point
    IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);
        stopped = false;
        Debug.Log(moving);
    }

    //Wait before an endpoint can be hit
    IEnumerator pointWait()
    {
        yield return new WaitForSeconds(waitTime + 1);
        stopPointDetection = false;
        Debug.Log("Pointwait over");
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
