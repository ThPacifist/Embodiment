using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorData : AntiChrist
{
    /*
     * Put on the parent to the object
     * Parent must have the same name
     */

    //Variables
    private bool isActive;

    //Constructor Function
    public override void Constructor()
    {
        savedObject = this.transform.FindChild(this.name).gameObject;
        isActive = savedObject.activeSelf;
    }

    //Rebuild Data Function
    public override void RebuildData()
    {
        isActive = savedObject.activeSelf;
    }

    //Reset Data Function
    public override void ResetData()
    {
        savedObject.SetActive(isActive);
    }
}
