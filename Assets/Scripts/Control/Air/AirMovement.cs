using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AirMovement : MonoBehaviour
{
    //Initial code added by Jason on 8/27
    /*
     * TODO: add a jump with  cooldown, but that does not require you to be on the ground.
     */

    //Assets and public variables
    AirControls input;
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
        if (input.AirMovement.Fly.ReadValue<float>() > 0)
        {
            Debug.Log("Fly");
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


}
