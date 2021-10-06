using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Checkpoint : MonoBehaviour
{
    //Public variables and assets
    public static Action<int> newCheckpoint = delegate { };
    public int ckptNum;

    //Private variables


    //When the player enters the trigger set the checkpoint
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            newCheckpoint(ckptNum);
        }
    }
}
