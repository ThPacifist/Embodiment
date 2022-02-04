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
    private Vector2 position;
    private bool isActive;

    //Constructor Function
    public override void InitializeData()
    {
        savedObject = this.transform.GetChild(0).gameObject;
        isActive = savedObject.activeSelf;
        position = savedObject.transform.position;
    }

    //Rebuild Data Function
    public override void SaveState()
    {
        isActive = savedObject.activeSelf;
        position = savedObject.transform.position;
    }

    //Reset Data Function
    public override void ResetData()
    {
        savedObject.SetActive(isActive);
        Debug.Log(savedObject.transform.position);
        savedObject.transform.position = position;
        Debug.Log(savedObject.transform.position);
    }
}
