using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : GameAction
{
    /*
     * Moves a platform to a specified point in space when a button is pressed
     */
    [SerializeField]
    Transform platform;
    [SerializeField]
    Transform activedPos;

    Vector2 restPos;
    public float speed = 1;
    float step;
    bool move;

    public override void Action()
    {

    }

    public override void Action(bool b)
    {
        move = !b;
    }

    private void Start()
    {
        restPos = platform.position;
    }

    private void Update()
    {
        step = speed * Time.deltaTime;
        if (move)
        {
            if (Vector2.Distance(platform.position, activedPos.position) > 0)
            {
                platform.position = Vector2.MoveTowards(platform.position, activedPos.position, step);
            }
        }
        else
        {
            if (Vector2.Distance(platform.position, restPos) > 0)
            {
                platform.position = Vector2.MoveTowards(platform.position, restPos, step);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(activedPos.position, 0.5f);
    }
}
