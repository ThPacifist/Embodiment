using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    /*
     * Description:
     * This script makes sure the player can't softlock by droppin a skeleton or box in the wrong spot.
     * If the object needs to be reset to the respawn point at the start, use that bool
     * Some things need to be killed in water, others don't, be smart about it
     */

    //Variables and assets
    public Transform skeleton;
    public Transform spawnPoint;
    public bool changeLocationAtStart = false;
    public bool waterKill;

    //If you want it to change locations at the start, use it
    private void Start()
    {
        if(changeLocationAtStart)
        {
            skeleton.position = spawnPoint.position;
        }
    }

    //Check if it has entered a killzone
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If it enters these tags, kill no matter what
        if(other.CompareTag("Death") || other.CompareTag("Shrieker"))
        {
            skeleton.position = spawnPoint.position;
        }
        //If it enters these, check if it should kill
        if(waterKill && other.CompareTag("Water"))
        {
            skeleton.position = spawnPoint.position;
        }
    }
}
