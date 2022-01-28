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
    int animState;

    //Constructor
    public override void Constructor()
    {
        savedObject = this.gameObject;
        switchScript = savedObject.GetComponent<Switch>();
        position = savedObject.transform.position;
        isTouching = switchScript.isTouching;
        active = switchScript.active;
        if(switchScript.Lever)
        {
            animState = switchScript.anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        }
    }

    //RebuildData
    public override void RebuildData()
    {
        position = savedObject.transform.position;
        isTouching = switchScript.isTouching;
        active = switchScript.active;
        if (switchScript.Lever)
        {
            animState = switchScript.anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        }
    }

    //ResetData
    public override void ResetData()
    {
        savedObject.transform.position = position;
        switchScript.isTouching = isTouching;
        switchScript.active = active;
        if (switchScript.Lever)
        {
            switchScript.anim.Play(animState);
        }
    }
}
