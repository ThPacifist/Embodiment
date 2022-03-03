using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAction : MonoBehaviour
{
    /*
     * Use this as a parent class
     * Using this script, we can control say, the switch scripts behavior that it triggers
     * Doing it this way allows us give the button different things it can trigger
     *      Like have a subclass that triggers an animation,
     *          Disables a script,
     *          Other types of behavior.
     *          
     * This allows us easily give scripts triggerable behaviors
     */

    public virtual void Action ()
    {

    }
    public virtual void Action (bool b)
    {

    }
    //Checks if the the inputed tag is any of the player's tag
    public static bool PlayerTags(string tag) //Use this to check for the player if using the tag
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
        else if (tag == "Cat")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Using the inputted collider places the gameobject on the floor
    public static Vector2 PlaceColOnGround(Collider2D col)
    {
        int layermask = LayerMask.NameToLayer("CheckSpace");
        RaycastHit2D hit = Physics2D.Raycast(col.transform.position, Vector2.down, Mathf.Infinity, layermask);

        if(hit.collider != null)
        {
            Vector2 Point = new Vector2(hit.point.x, hit.point.y);
            return Point;
        }
        return col.transform.position;
    }
}
