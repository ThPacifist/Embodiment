using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorData : BaseData
{
    /*
     * Put on the parent to the object
     * Parent must have the same name
     */

    //Variables
    private bool isActive;

    //Constructor Function
    public override void InitializeData()
    {
        savedObject = this.transform.GetChild(0).gameObject;
        isActive = savedObject.activeSelf;
    }

    //Rebuild Data Function
    public override void SaveState()
    {
        isActive = savedObject.activeSelf;
    }

    //Reset Data Function
    public override void ResetData()
    {
        savedObject.SetActive(isActive);
    }
}
