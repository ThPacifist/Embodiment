using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAction : MonoBehaviour
{
    public virtual void Action ()
    {

    }
    public virtual void Action (bool b)
    {

    }

    public static bool PlayerTags(string tag)
    {
        if (tag == "Blob")
        {
            return true;
        }
        else if (tag == "Human")
        {
            return true;
        }
        else if (tag == "Bat")
        {
            return true;
        }
        else if (tag == "Fish")
        {
            return true;
        }
        else if (tag == "Human")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
