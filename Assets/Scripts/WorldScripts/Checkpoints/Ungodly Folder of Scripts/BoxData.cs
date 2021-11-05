using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxData : AntiChrist
{
    //Variables
    private Vector2 position;
    private Quaternion rotation;
    private bool carried;

    //Constructor function
    public override void Constructor()
    {
        savedObject = this.gameObject;
        position = savedObject.transform.position;
        rotation = savedObject.transform.rotation;
    }

    //Rebuild data function
    public override void RebuildData()
    {
        position = savedObject.transform.position;
        rotation = savedObject.transform.rotation;
        if(GameAction.PlayerTags(savedObject.transform.parent.tag))
        {
            carried = true;
        }
        else
        {
            carried = false;
        }
    }

    //Reset data function
    public override void ResetData()
    {
        if(!carried)
        {
            savedObject.transform.position = position;
            savedObject.transform.rotation = rotation;
        }
    }
}
