using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSprite : GameAction
{
    public GameObject Sprite;
    public override void Action()
    {
        Toggle();
    }
    public override void Action(bool b)
    {
        Toggle(b);
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
