using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpozyteMove : MonoBehaviour
{
    /*
     * Description:
     * This function handles the movement of Expozyte
     * It is currently in a state of flux due to how early it is
     */

    //Public variables
    public Transform[] Checkpoints;
    public Transform Expozyte;
    public int atCheckpoint;
    public int toCheckpoint;
    public bool moving;

    //Private variables

    //Fixed update is called 60 times a second
    private void FixedUpdate()
    {
        //If he should be moving, move him
        if(moving)
        {
            Move();
        }
    }

    //This function moves expozyte
    private void Move()
    {
        //If he is not there yet, move him there
        if(Expozyte.position != Checkpoints[toCheckpoint].position)
        {
            Expozyte.position = Checkpoints[toCheckpoint].position;
        }
        //If he is, change his values
        else
        {
            atCheckpoint = toCheckpoint;
            toCheckpoint = -1;
            moving = false;
        }
    }
}
