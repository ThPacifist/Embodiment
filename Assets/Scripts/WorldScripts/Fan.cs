using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : GameAction
{
    /*
     * Description:
     * This script controls the fan's direction and handles
     * when the fan is turned off and animations.
     */

    //Public variables and assets
    public Animator FanAnimator;
    public AreaEffector2D effector;
    public bool facingRight;
    public bool startsOn;

    //Private values and assets
    private bool on;
    private float force;
    private bool buttonPressed;

    //Called on start
    public void Start()
    {
        //Set the animatins
        FanAnimator.SetBool("On", startsOn);
        on = startsOn;
        //Set the force drection
        if(!facingRight)
        {
            effector.forceMagnitude *= -1;
        }
        //Get the force
        force = effector.forceMagnitude;
    }

    //Called when an associated button switches state
    public override void Action(bool var)
    {
        Debug.Log("Fan Script");
        if (!buttonPressed)
        {
            //Set on variable and animation
            on = !on;
            FanAnimator.SetBool("On", on);
            //Set area effector
            if (on)
            {
                effector.forceMagnitude = force;
            }
            else
            {
                effector.forceMagnitude = 0;
            }
            buttonPressed = true;
        }
    }

}
