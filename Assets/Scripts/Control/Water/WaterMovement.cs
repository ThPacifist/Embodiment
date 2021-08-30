using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterMovement : MonoBehaviour
{
    //Initial code by Jason on 8/27
    /*
     * TODO:
     * Add functionality to jump
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
        player.position += a;

        //Jump
        if (input.WaterMovement.Jump.ReadValue<float>() > 0)
        {
            Debug.Log("Jump");
        }

        //Interact
        if (input.WaterMovement.Interact.ReadValue<float>() > 0)
        {
            Interact();
        }

        //Embody
        if (input.WaterMovement.Embody.ReadValue<float>() > 0)
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
}
