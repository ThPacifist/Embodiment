using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSprite : GameAction
{
    public GameObject Sprite;
    public override void Action()//This overrides the virtual Action function in the GameAction script
    {
        Toggle();// Toggles the sprite between off and on
    }
    public override void Action(bool b)//This overrides the virtual Action(bool b) function in the GameAction script
    {
        Toggle(b);// Using the passed in bool, toggles the sprite on and off
    }

    void Toggle()
    {
        if (Sprite.activeSelf)
        {
            Sprite.SetActive(false);
        }
        else
        {
            Sprite.SetActive(true);
        }
    }

    void Toggle(bool b)
    {
        if(b)
        {
            Sprite.SetActive(true);
        }
        else
        {
            Sprite.SetActive(false);
        }
    }
}
