using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class LandMovement : MonoBehaviour
{
    //Initial code added by Jason on 8/26
    //Jump added by Jason on 8/30
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
    public bool isGrounded = false;
    public float speed = 10;
    public float jumpHeight = 2;
    //Private variables


    //Things to do on awake
    private void Awake()
    {
        input = new LandControls();
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
        player.position += new Vector3(1, 0, 0) * input.LandMovement.Movement.ReadValue<float>() * speed * Time.deltaTime;

        //Jump
        if (input.LandMovement.Jump.ReadValue<float>() > 0 && isGrounded == true)
        {
            isGrounded = false;
            rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
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

        //Special interact
        if (input.LandMovement.Special.ReadValue<float>() > 0)
        {
            Special();
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
}
