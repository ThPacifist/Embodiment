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
    private float currentPos;
    private int moveTowards;
    private bool moving;

    //Constructor Function
    public override void Constructor()
    {
        savedObject = this.gameObject.transform.FindChild(this.name).gameObject;
        selectedPlatform = savedObject.GetComponent<MovingPlatforms>();
        moving = selectedPlatform.moving;
        moveTowards = selectedPlatform.moveTowards;
        currentPos = selectedPlatform.currentPos;
        position = savedObject.transform.position;
    }

    //Rebuild Data Function
    public override void RebuildData()
    {
        moving = selectedPlatform.moving;
        moveTowards = selectedPlatform.moveTowards;
        currentPos = selectedPlatform.currentPos;
        position = savedObject.transform.position;
    }

    //Reset Data Function
    public override void ResetData()
    {
        selectedPlatform.moving = moving;
        selectedPlatform.moveTowards = moveTowards;
        selectedPlatform.currentPos = currentPos;
        savedObject.transform.position = position;
    }
}
