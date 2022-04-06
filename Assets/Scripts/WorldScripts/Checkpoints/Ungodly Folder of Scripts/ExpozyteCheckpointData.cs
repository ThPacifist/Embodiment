using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpozyteCheckpointData : BaseData
{
    //Variables
    public BoxCollider2D trigger;
    private bool active;

    //Constructor function
    public override void InitializeData()
    {
        trigger = this.gameObject.GetComponent<BoxCollider2D>();
        active = trigger.isActiveAndEnabled;
    }

    //Rebuild data
    public override void SaveState()
    {
        active = trigger.isActiveAndEnabled;
    }

    //Reset data
    public override void ResetData()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = active;
    }
}
