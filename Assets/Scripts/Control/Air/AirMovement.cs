using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AirMovement : MonoBehaviour
{
    //Initial code added on 8/27 by Jason 
    //Jumpng added on 8/30 by Jason
    //Basic functionality finished on 8/31 by Jason
    /*
     * TODO:
     * Make it float on water
     * Tweaks to movement as the game goes on
     */

    //Assets and public variables
    AirControls input;
    public Transform player;
    public Rigidbody rigid;
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Special = delegate { };
    public static Action Pause = delegate { };
    private float speed = 10;
    private float jumpHeight = 3;
    //Private variables
    public float cooldown;
    public bool canJump = true;

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
        player.position += new Vector3(1, 0, 0) * input.AirMovement.Movement.ReadValue<float>() * speed * Time.deltaTime;

        //Jump
        if (input.AirMovement.Fly.ReadValue<float>() > 0 && canJump)
        {
            rigid.AddForce((Vector3.up * jumpHeight) - rigid.velocity, ForceMode.Impulse);
            canJump = false;
            cooldown = 0;
        }
        StartCoroutine("FlyCoolDown");

        //Limit height
        if(player.position.y > 100)
        {
            rigid.velocity = Vector3.down;
            rigid.angularVelocity = Vector3.down;
        }

        //Interact
        if (input.AirMovement.Interact.ReadValue<float>() > 0)
        {
            Interact();
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

    IEnumerator FlyCoolDown()
    {
        while(!canJump)
        {
            yield return new WaitForEndOfFrame();
            cooldown += Time.deltaTime;
            if(cooldown > 2)
            {
                canJump = true;
                cooldown = 0;
            }
        }
    }
}
