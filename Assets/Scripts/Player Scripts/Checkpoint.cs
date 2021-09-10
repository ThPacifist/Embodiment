using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //Created by Jason on 9/10

/*
 * TODO:
 */

    //Pubilc variables
    public Transform[] checkpoints;
    public Transform player;
    public int toCheckpoint = 0;

    //Private variables
    private int previousCheckpoint = 0;

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    //Update
    private void Update()
    {
        if(toCheckpoint != previousCheckpoint)
        {
            player.position = checkpoints[toCheckpoint].position;
            previousCheckpoint = toCheckpoint;
        }
    }

    //Gets called whenever the player gets moved
    private void moveToCheckpoint(int newPosition)
    {
        player.position = checkpoints[newPosition].position;
    }

}
