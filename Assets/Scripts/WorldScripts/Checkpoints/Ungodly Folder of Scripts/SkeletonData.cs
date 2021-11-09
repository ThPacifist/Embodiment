using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonData : AntiChrist
{
    //Variables
    private Vector2 position;

    //Constructor
    public override void Constructor()
    {
        savedObject = this.gameObject.transform.GetChild(1).gameObject;
        position = savedObject.transform.position;
    }

    //Rebuild Data
    public override void RebuildData()
    {
        position = savedObject.transform.position;
    }
    //Reset Data
    public override void ResetData()
    {
        savedObject.transform.position = position;
    }
}
