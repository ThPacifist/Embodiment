using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratchable : MonoBehaviour
{
    /*
     * Description:
     * This script makes an object breakable by the cat scratch.
     * - The bool 'breakObj' will make the object break instead of switching sprites
     * - Note that the object does not have to be the same as the object hit by the cat
     */

    //Public variables and assets
    public GameObject scratableObj;
    public int health;
    public bool breakObj = false;

    //Private variables 
    private bool inRange;

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        SpecialInteractions.Scratch += attacked;
    }

    private void OnDisable()
    {
        SpecialInteractions.Scratch -= attacked;
    }

    //This function will break the object
    private void breakObject()
    {
        //Break the object/play animation to break object
        Destroy(scratableObj);
        //Disable this script for if this is not a part of the object
    }

    //Gets called when this object is attacked
    private void attacked()
    {
        if(inRange)
        {
        //Reduce health
        health--;
        //Check if it is done
        if(health <= 0)
        {
            //Break object
            if(breakObj)
            {
                breakObject();
            }
            else
            {
                //switchSprites();
            }
        }
        }
    }


    //Check for if it in the area to be hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
        {
            Debug.Log("Player attack entered range");
            //Set inRange
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
        {
            Debug.Log("Player attack left range");
            //Unset inRange
            inRange = false;
        }
    }
}
