using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformData : AntiChrist
{
    /*
     * Put on the parent to this object
     * Parent must have the same name
     */

    //Variables
    private MovingPlatforms selectedPlatform;
    private Vector2 position;
    private Vector2 currentPos;
    private int moveTowards;
    private bool moving;

    //Constructor Function
    public override void Constructor()
    {
        savedObject = this.gameObject.transform.GetChild(0).gameObject;
        selectedPlatform = savedObject.GetComponent<MovingPlatforms>();
        moving = selectedPlatform.moving;
        moveTowards = selectedPlatform.moveTowards;
        currentPos = selectedPlatform.transform.position;
        position = savedObject.transform.position;
    }

    //Rebuild Data Function
    public override void RebuildData()
    {
        moving = selectedPlatform.moving;
        moveTowards = selectedPlatform.moveTowards;
        currentPos = selectedPlatform.transform.position;
        position = savedObject.transform.position;
    }

    //Reset Data Function
    public override void ResetData()
    {
        selectedPlatform.moving = moving;
        selectedPlatform.moveTowards = moveTowards;
        selectedPlatform.transform.position = currentPos;
        savedObject.transform.position = position;
        selectedPlatform.stopped = false;
    }
}
