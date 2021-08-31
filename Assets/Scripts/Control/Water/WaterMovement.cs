using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterMovement : MonoBehaviour
{
    //Initial code by Jason on 8/27
    //Water gated stated added by Jason on 8/31
    /*
     * TODO:
     * Add jump
     * Tweaks to the movement as the game goes on
     */

    //Assets and public variables
    WaterControls input;
    public Transform player;
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Special = delegate { };
    public static Action Pause = delegate { };

    //Private variables
    private float speed = 10;
    private bool inWater = false;
    private bool surface = false;
    private bool onLand = false;

    //Things to do on awake
    private void Awake()
    {
        input = new WaterControls();
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
        //Movement for left and right and up and down when in water
        Vector3 a = input.WaterMovement.Movement.ReadValue<Vector2>() * speed * Time.deltaTime;

        //If not in water don't allow vertical movement
        if(!inWater)
        {
            a.y = 0;
        }

        //If on land slow down movement
        if(onLand)
        {
            a.x *= 0.5f;
        }

        player.position += a;

        //Jump
        if (input.WaterMovement.Jump.ReadValue<float>() > 0 && surface)
        {
            Debug.Log("Jump");
        }

        //Interact
        if (input.WaterMovement.Interact.ReadValue<float>() > 0)
        {
            Interact();
        }

        //Embody
        if (input.WaterMovement.Embody.ReadValue<float>() > 0 && onLand)
        {
            Embody();
        }

        //Special interact
        if (input.WaterMovement.Special.ReadValue<float>() > 0)
        {
            Special();
        }

        //Pause
        if (input.WaterMovement.Pause.ReadValue<float>() > 0)
        {
            Pause();
        }

    }

    //Check if they're on the ground
    private void OnCollisionStay(Collision collision)
    {
        if (!inWater)
        {
            onLand = true;
        }
    }
}
