using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSidedWallTrigger : GameAction
{
    /*
     * Use the right and left bools to tell which side the player should be coming out of and be blocked by the wall
     */

    [Tooltip("Check this if the side of the wall is on the right.")]
    public bool right;
    [Tooltip("Check this if the side of the wall is on the left.")]
    public bool left;

    [SerializeField]
    GameAction behavior;
    [SerializeField]
    Collider2D col;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(GameAction.PlayerTags(collision.tag))
        {
            if (right)
            {
                if (collision.transform.position.x > col.bounds.max.x)
                {
                    if (behavior)//If behavior is not null
                        behavior.Action(false);
                }
                else if (left)
                {
                    if (collision.transform.position.x < col.bounds.min.x)
                    {
                        if (behavior)//If behavior is not null
                            behavior.Action(false);
                    }
                }
            }
        }
    }
}
