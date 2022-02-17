using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxData : BaseData
{
    //Variables
    private Vector2 position;
    private Quaternion rotation;
    private bool carried;

    //Constructor function
    public override void InitializeData()
    {
        savedObject = this.gameObject;
        position = savedObject.transform.position;
        rotation = savedObject.transform.rotation;
    }

    //Rebuild data function
    public override void SaveState()
    {
        position = savedObject.transform.position;
        rotation = savedObject.transform.rotation;
        if (savedObject.transform.parent != null)
        {
            if (GameAction.PlayerTags(savedObject.transform.parent.tag))
            {
                carried = true;
            }
            else
            {
                carried = false;
            }
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
