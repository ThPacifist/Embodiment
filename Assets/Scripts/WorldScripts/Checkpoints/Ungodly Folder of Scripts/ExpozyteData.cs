using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpozyteData : BaseData
{
    //Variables
    public ExpozyteMove expMove;
    public Transform expozyte;
    private Transform[] checkpoints;
    private int currentCheckpoint;
    
    //Constructor function
    public override void InitializeData()
    {
        checkpoints = expMove.Checkpoints;
        currentCheckpoint = 0;
    }

    //Rebuild data function
    public override void SaveState()
    {
        if (expMove.toCheckpoint != -1)
        {
            currentCheckpoint = expMove.toCheckpoint;
        }
        else
        {
            currentCheckpoint = expMove.atCheckpoint;
        }
    }

    //Reset data function
    public override void ResetData()
    {
        expozyte.position = checkpoints[currentCheckpoint].position;
        expMove.atCheckpoint = currentCheckpoint;
        expMove.toCheckpoint = -1;
        expMove.moving = false;
    }
}
