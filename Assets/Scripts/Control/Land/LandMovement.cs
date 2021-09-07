using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class LandMovement : MonoBehaviour
{
    //Initial code added by Jason on 8/26
    //Jump added by Jason on 8/30
    //Changed to velocity based movement by Jason on 9/3
    /* 
     * TODO:
     * Make it float on water
     * Tweaks to movement as the game goes on
     */

    //Assets and public variables
    LandControls input;
    public Transform player;
    public Rigidbody rigid;
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Special = delegate { };
    public static Action Pause = delegate { };
    public float speed;
    public float jumpHeight = 2;
    
    //Private variables
    private bool catClimb = false;
    private bool isGrounded = false;

    //Things to do on awake
    private void Awake()
    {
        input = new LandControls();
    }

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        input.Enable();
        SpecialInteractions.Climb += Climb;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    //Gets called on each frame
    private void Update()
    {
        //Special interact
        if (input.LandMovement.Special.ReadValue<float>() > 0)
        {
            Special();
        }

        //When the cat is climbing it cannot do those
        if (!catClimb)
        {
            //Jump
            if (input.LandMovement.Jump.ReadValue<float>() > 0 && isGrounded == true)
            {
                isGrounded = false;
                rigid.AddForce((Vector3.up * jumpHeight) - new Vector3(0, rigid.velocity.y, 0), ForceMode.Impulse);
            }

            //Interact
            if (input.LandMovement.Interact.ReadValue<float>() > 0)
            {
                Interact();
            }

            //Embody
            if (input.LandMovement.Embody.ReadValue<float>() > 0)
            {
                Embody();
            }

            //Movement for left and right
            if (input.LandMovement.Movement.ReadValue<float>() != 0)
            {
                rigid.velocity += (Vector3.right * input.LandMovement.Movement.ReadValue<float>() * speed) - new Vector3(rigid.velocity.x, 0, 0);
            }

        }
        else
        {
            player.position += new Vector3(0, 1, 0) * input.LandMovement.Movement.ReadValue<float>() * speed * Time.deltaTime;
        }

        //Pause
        if(input.LandMovement.Pause.ReadValue<float>() > 0)
        {
            Pause();
        }

    }

    //Check if they're on the ground
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    //When the cat starts climbing, things need to happen here
    private void Climb()
    {
        if(catClimb)
        {
            catClimb = false;
        }
    }
}
