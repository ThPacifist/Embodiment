using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformData : BaseData
{
    /*
     * Put on the parent to this object
     * Parent must have the same name
     */

    //Variables
    private MovingPlatforms selectedPlatform;
    private Vector2 position;
    private Vector2 currentPos;
    private float speed;
    private int moveTowards;
    private bool moving;
    private bool stopped;

    //Constructor Function
    public override void InitializeData()
    {
        savedObject = this.gameObject.transform.GetChild(0).gameObject;
        selectedPlatform = savedObject.GetComponent<MovingPlatforms>();
        moving = selectedPlatform.moving;
        stopped = selectedPlatform.stopped;
        speed = selectedPlatform.speed;
        moveTowards = selectedPlatform.moveTowards;
        currentPos = selectedPlatform.transform.position;
        position = savedObject.transform.position;
    }

    //Rebuild Data Function
    public override void SaveState()
    {
        moving = selectedPlatform.moving;
        moveTowards = selectedPlatform.moveTowards;
        currentPos = selectedPlatform.transform.position;
        position = savedObject.transform.position;
        stopped = selectedPlatform.stopped;
        speed = selectedPlatform.speed;
    }

    //Reset Data Function
    public override void ResetData()
    {
        selectedPlatform.moving = moving;
        selectedPlatform.moveTowards = moveTowards;
        selectedPlatform.transform.position = currentPos;
        savedObject.transform.position = position;
        selectedPlatform.speed = speed;
        if(stopped)
        {
            selectedPlatform.StopAllCoroutines();
            selectedPlatform.stopped = stopped;
            selectedPlatform.StartCoroutine("wait");
        }
    }
}