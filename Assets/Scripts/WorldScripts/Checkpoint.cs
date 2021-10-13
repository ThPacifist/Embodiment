using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Checkpoint : MonoBehaviour
{
    /*
     * Description:
     * This script detects when a player enters the appropriate checkpoint and sends that to the controller
     */
    //Public variables and assets
    public static Action<int> newCheckpoint = delegate { };
    public int ckptNum;

    //Private variables


    //When the player enters the trigger set the checkpoint
    public void OnTriggerEnter2D(Collider2D other)
    {
        //If a player enters the checkpoint send the number of the checkpoint to the controller
        if (other.CompareTag("Blob") ||other.CompareTag("Fish") ||other.CompareTag("Human") ||other.CompareTag("Cat") ||other.CompareTag("Bat"))
        {
            newCheckpoint(ckptNum);
        }
    }
}
