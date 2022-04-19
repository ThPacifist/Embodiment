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
    }

    //Called when an associated button switches state
    public override void Action()
    {
        on = !on;
        FanAnimator.SetBool("On", on);
    }

}
