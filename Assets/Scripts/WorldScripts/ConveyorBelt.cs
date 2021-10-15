using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : GameAction
{
    [SerializeField]
    SurfaceEffector2D surfaceEff;

    [SerializeField]
    bool toggleOnOff;
    [SerializeField]
    bool switchDirection;

    public override void Action()
    {
        if (toggleOnOff)
        {
            SwitchOnOff();
        }

        if (switchDirection)
        {
            SwitchDirection();
        }
    }

    public override void Action(bool b)
    {
        if (toggleOnOff)
        {
            SwitchOnOff(b);
        }

        if(switchDirection)
        {
            SwitchDirection(b);
        }
    }

    void SwitchDirection()
    {
        if(surfaceEff.speed > 0)
        {
            surfaceEff.speed *= -1;
        }
        else if(surfaceEff.speed < 0)
        {
            surfaceEff.speed = Mathf.Abs(surfaceEff.speed);
        }
    }

    void SwitchDirection(bool b)
    {
        if (b)
        {
            if (surfaceEff.speed > 0)
            {
                surfaceEff.speed *= -1;
            }
            else if (surfaceEff.speed < 0)
            {
                surfaceEff.speed = Mathf.Abs(surfaceEff.speed);
            }
        }
        else
        {
            if (surfaceEff.speed > 0)
            {
                surfaceEff.speed *= -1;
            }
            else if (surfaceEff.speed < 0)
            {
                surfaceEff.speed = Mathf.Abs(surfaceEff.speed);
            }
        }
    }

    void SwitchOnOff()
    {
        if (surfaceEff.isActiveAndEnabled)
        {
            surfaceEff.enabled = false;
        }
        else
        {
            surfaceEff.enabled = true;
        }
    }

    void SwitchOnOff(bool b)
    {
        if (b)
        {
            surfaceEff.enabled = true;
        }
        else
        {
            surfaceEff.enabled = false;
        }
    }
}
