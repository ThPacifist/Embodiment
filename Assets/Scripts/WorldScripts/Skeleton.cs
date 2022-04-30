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

    //If you want it to change locations at the start, use it
    private void Start()
    {
        if(changeLocationAtStart)
        {
            RespawnSkeleton();
        }
    }

    //Check if it has entered a killzone
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If it enters these tags, kill no matter what
        if(other.CompareTag("Death") || other.CompareTag("Shrieker") || other.CompareTag("Water"))
        {
            RespawnSkeleton();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Shrieker"))
        {
            RespawnSkeleton();
        }
    }

    public void RespawnSkeleton()
    {
        skeleton.position = spawnPoint.position;
    }
}
