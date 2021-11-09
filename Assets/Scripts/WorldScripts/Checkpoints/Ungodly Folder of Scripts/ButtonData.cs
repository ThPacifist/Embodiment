using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonData : AntiChrist
{
    //Variables
    Switch switchScript;
    Vector2 position;
    bool isTouching;
    bool active;

    //Constructor
    public override void Constructor()
    {
        savedObject = this.gameObject;
        switchScript = savedObject.GetComponent<Switch>();
        position = savedObject.transform.position;
        isTouching = switchScript.isTouching;
        active = switchScript.active;
    }

    //RebuildData
    public override void RebuildData()
    {
        position = savedObject.transform.position;
        isTouching = switchScript.isTouching;
        active = switchScript.active;
    }

    //ResetData
    public override void ResetData()
    {
        savedObject.transform.position = position;
        switchScript.isTouching = isTouching;
        switchScript.active = active;
    }
}
