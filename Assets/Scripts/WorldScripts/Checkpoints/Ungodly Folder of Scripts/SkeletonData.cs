using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonData : BaseData
{
    //Variables
    private Vector2 position;

    //Constructor
    public override void InitializeData()
    {
        savedObject = this.gameObject.transform.GetChild(1).gameObject;
        position = savedObject.transform.position;
    }

    //Rebuild Data
    public override void SaveState()
    {
        position = savedObject.transform.position;
    }
    //Reset Data
    public override void ResetData()
    {
        savedObject.transform.position = position;
    }
}
