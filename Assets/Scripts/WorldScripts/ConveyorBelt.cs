using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : GameAction
{
    [SerializeField]
    SurfaceEffector2D surfaceEff;
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    Animator conveyorAnim;

    [Header("Button Actions")]
    [Tooltip("Makes a switch the conveyor belt ON or OFF")]
    [SerializeField]
    bool toggleOnOff;
    [Tooltip("Makes a switch reverse the direction the conveyor belt moves objects")]
    [SerializeField]
    bool switchDirection;

    bool value;

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

    /// <summary>
    /// These functions switch the direction of the conveyor belt
    /// </summary>
    void SwitchDirection()
    {
        value = sprite.flipX;

        surfaceEff.speed *= -1;

        sprite.flipX = !value;
    }

    void SwitchDirection(bool b)
    {
        value = sprite.flipX;

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

        sprite.flipX = !value;
    }

    /// <summary>
    /// These functions disable the conveyor belt when called
    /// </summary>
    void SwitchOnOff()
    {
        if (surfaceEff.isActiveAndEnabled)
        {
            surfaceEff.enabled = false;
            conveyorAnim.enabled = false;
            sprite.color = Color.gray;
        }
        else
        {
            surfaceEff.enabled = true;
            conveyorAnim.enabled = true;
            sprite.color = Color.white;
        }
    }

    void SwitchOnOff(bool b)
    {
        if (b)
        {
            surfaceEff.enabled = true;
            conveyorAnim.enabled = true;
            sprite.color = Color.white;
        }
        else
        {
            surfaceEff.enabled = false;
            conveyorAnim.enabled = false;
            sprite.color = Color.gray;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 center = transform.position + Vector3.up;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(center + new Vector3(-0.5f, 0, 0), center + new Vector3(0.5f, 0, 0));
        if(surfaceEff.speed > 0)
        {
            Vector3 tip = center + new Vector3(0.5f, 0, 0);
            Gizmos.DrawLine(tip, tip + new Vector3(-0.25f, 0.25f, 0));
            Gizmos.DrawLine(tip, tip + new Vector3(-0.25f, -0.25f, 0));
            Gizmos.DrawLine(tip + new Vector3(-0.25f, 0.25f, 0), tip + new Vector3(-0.25f, -0.25f, 0));
        }
        else if(surfaceEff.speed < 0)
        {
            Vector3 tip = center + new Vector3(-0.5f, 0, 0);
            Gizmos.DrawLine(tip, tip + new Vector3(0.25f, 0.25f, 0));
            Gizmos.DrawLine(tip, tip + new Vector3(0.25f, -0.25f, 0));
            Gizmos.DrawLine(tip + new Vector3(0.25f, 0.25f, 0), tip + new Vector3(0.25f, -0.25f, 0));
        }
    }
}
