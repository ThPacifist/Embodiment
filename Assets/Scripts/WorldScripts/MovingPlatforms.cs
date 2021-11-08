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
    public float currentPos;
    public int moveTowards;

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
                //Stop
                moving = false;
                StartCoroutine("wait");
                Debug.Log(moving);
            }
        }    
        
    }

    //Wait after hitting an end point
    IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);
        moving = true;
        Debug.Log(moving);
    }

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
}
