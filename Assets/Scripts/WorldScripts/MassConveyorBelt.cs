using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassConveyorBelt : GameAction
{
    [SerializeField]
    ConveyorBelt[] conveyorBelts;

    [SerializeField]
    bool toggleOnOff;
    [SerializeField]
    bool switchDirection;

    public override void Action(bool b)
    {
        if (toggleOnOff)
        {
            SwitchOnOff(b);
        }

        if (switchDirection)
        {
            SwitchDirection(b);
        }
    }

    void SwitchDirection(bool b)
    {
        foreach(ConveyorBelt conveyorbelt in conveyorBelts)
        {
            conveyorbelt.Action(b);
        }
    }

    void SwitchOnOff(bool b)
    {
        foreach (ConveyorBelt conveyorbelt in conveyorBelts)
        {
            conveyorbelt.Action(b);
        }
    }
}
