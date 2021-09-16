using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AirMovement : MonoBehaviour
{
    //Initial code added on 8/27 by Jason 
    //Jumpng added on 8/30 by Jason
    //Basic functionality finished on 8/31 by Jason
    //Changed to velocity based movement by Jason on 9/3
    /*
     * TODO:
     * Make it float on water
     * Tweaks to movement as the game goes on
     */

    //Assets and public variables
    AirControls input;
    public Transform player;
    public Rigidbody2D rigid;
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Special = delegate { };
    public static Action Pause = delegate { };
    public float cooldown = 0.1f;
    public float speed = 5;
    public float jumpHeight = 5;

    //Private variables
    private bool canJump = true;

    //Things to do on awake
    private void Awake()
    {
        input = new AirControls();
    }

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    //Gets called on each frame
    private void Update()
    {
        //Movement for left and right
        if (input.AirMovement.Movement.ReadValue<float>() != 0)
        {
            rigid.velocity += (Vector2.right * input.AirMovement.Movement.ReadValue<float>() * speed) - new Vector2(rigid.velocity.x, 0);
        }

        //Interact
        if (input.AirMovement.Interact.ReadValue<float>() > 0)
        {
            Interact();
        }

        //Jump
        if (input.AirMovement.Fly.ReadValue<float>() > 0 && canJump)
        {
            rigid.AddForce((Vector2.up * jumpHeight) - new Vector2(0, rigid.velocity.y), ForceMode2D.Impulse);
            canJump = false;
            StartCoroutine("FlyCoolDown");
        }

        //Embody
        if (input.AirMovement.Embody.ReadValue<float>() > 0)
        {
            Embody();
        }

        //Special interact
        if (input.AirMovement.Special.ReadValue<float>() > 0)
        {
            Special();
        }

        //Pause
        if (input.AirMovement.Pause.ReadValue<float>() > 0)
        {
            Pause();
        }

    }

    //Cooldown for jumping in midair
    IEnumerator FlyCoolDown()
    {
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }
}
