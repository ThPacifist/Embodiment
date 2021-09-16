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
    public Transform attackBox;
    public Rigidbody2D rigid;
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Special = delegate { };
    public static Action Pause = delegate { };
    public float speed;
    public float jumpHeight;

    //Private variables
    private Vector2 look;
    private bool catClimb = false;
    public bool isGrounded = false;
    private bool pushing = false;

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

        //Jump
        if (input.LandMovement.Jump.ReadValue<float>() > 0 && isGrounded == true)
        {
            isGrounded = false;
            catClimb = false;
            rigid.gravityScale = 1;
            rigid.AddForce((Vector2.up * jumpHeight) - new Vector2(0, rigid.velocity.y), ForceMode2D.Impulse);
        }

        //When the cat is climbing it cannot do those
        if (!catClimb)
        {
            //When anyone is pushing, it can only move and interact
            if (!pushing)
            {
                //Embody
                if (input.LandMovement.Embody.ReadValue<float>() > 0)
                {
                    Embody();
                }
            }

            //Interact
            if (input.LandMovement.Interact.ReadValue<float>() > 0)
            {
                Interact();
            }

            //Movement for left and right
            if (input.LandMovement.Movement.ReadValue<float>() != 0)
            {
                //Movement algorithm
                rigid.velocity += (Vector2.right * input.LandMovement.Movement.ReadValue<float>() * speed) - new Vector2(rigid.velocity.x, 0);
                //Which direction should the cat attack go
                if(input.LandMovement.Movement.ReadValue<float>() > 0)
                {
                    attackBox.position = player.position + new Vector3(1, 0.5f, 0);
                }
                else
                {
                    attackBox.position = player.position + new Vector3(-1, 0.5f, 0);
                }
            }

        }
        //Climb
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    //When the cat starts climbing, things need to happen here
    private void Climb()
    {
        if(catClimb)
        {
            catClimb = false;
            rigid.gravityScale = 1;
        }
        else
        {
            catClimb = true;
            rigid.gravityScale = 0;
        }
    }
}
