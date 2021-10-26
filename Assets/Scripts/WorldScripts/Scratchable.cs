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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if it is the attack
        if(collision.CompareTag("PlayerAttack"))
        {
            attacked();
        }
    }
}
