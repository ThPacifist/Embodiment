using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    /*
     * Description:
     * This script sends the player to the previous checkpoint when needed by other scripts
     */
    //Pubilc variables
    public Transform[] checkpoints;
    public Transform player;
    public int toCheckpoint = 0;

    //Private variables
    public int previousCheckpoint = 0;

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        Checkpoint.newCheckpoint += UpdateCheckpoint;
    }

    private void OnDisable()
    {
        Checkpoint.newCheckpoint -= UpdateCheckpoint;
    }

    //Update
    private void Update()
    {
        //Debug mode change checkpoints, add slashes to * for testing
        /*
         *
        if(toCheckpoint != previousCheckpoint)
        {
            player.position = checkpoints[toCheckpoint].position;
            previousCheckpoint = toCheckpoint;
        }
        *
        */
        if(!player.gameObject.activeSelf)
        {
            MoveToCheckpoint(previousCheckpoint);
            player.gameObject.SetActive(true);
        }
    }

    //Gets called whenever the player gets moved
    public void MoveToCheckpoint(int newPosition)
    {
        player.position = checkpoints[newPosition].transform.position;
    }

    //Update checkpoint number
    public void UpdateCheckpoint(int newPosition)
    {
        previousCheckpoint = newPosition;
    }

}
