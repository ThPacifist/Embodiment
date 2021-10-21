using CMF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformGA : GameAction
{
    public MovingPlatform mPlatform;

    public override void Action()
    {
        mPlatform.StartPlatformMovement();
    }
}
