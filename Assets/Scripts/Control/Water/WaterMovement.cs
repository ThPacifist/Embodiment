using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterMovement : MonoBehaviour
{
    //Initial code by Jason on 8/27
    //Water gated states variables added by Jason on 8/31
    //Changed to velocity based by Jason on 9/3
    /*
     * TODO:
     * Add jump
     * Tweaks to the movement as the game goes on
     */
    //Boolean variables
    [SerializeField]
    bool inWater = false;
    [SerializeField]
    bool onLand = false;
    [SerializeField]
    bool aboveWater = false;

    //Assets and public variables
    WaterControls input;
    public Transform player;
    public Rigidbody rigid;
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Special = delegate { };
    public static Action Pause = delegate { };
    public Rigidbody rb;

    public float speed = 10;
    public float jumpHeight = 2;
    public float velocityX;
    public float velocityY;
    //Private variables


    //Things to do on awake
    private void Awake()
    {
        input = new WaterControls();
        rb = this.GetComponent<Rigidbody>();

        if(inWater)
        {
            rb.useGravity = false;
        }
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
        velocityX = rb.velocity.x;
        velocityY = rb.velocity.y;

       //Movement 
        if (input.WaterMovement.Movement.ReadValue<float>() != 0)
        {
            rigid.velocity += (Vector3.right * input.WaterMovement.Movement.ReadValue<float>() * speed) - rigid.velocity;
        }

    //If not in water don't allow vertical movement
        if (!inWater)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, 0, 0);
        }

        //If on land slow down movement
        if(onLand)
        {
            rigid.velocity *= 0.5f;
        }

        //Jump
        if (input.WaterMovement.Jump.ReadValue<float>() > 0 && onLand)
        {
            onLand = false;
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

        //Interact
        if (input.WaterMovement.Interact.ReadValue<float>() > 0)
        {
            Interact();
        }

        //Embody
        if (input.WaterMovement.Embody.ReadValue<float>() > 0 && aboveWater)
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = true;
            onLand = false;
            aboveWater = false;
            rb.useGravity = false;
            StartCoroutine(delayVelocity());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;
            aboveWater = true;
            StartCoroutine(delayGravity());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (aboveWater)
        {
            onLand = true;
        }
    }

    IEnumerator delayGravity()
    {
        yield return new WaitForSeconds(2);
        rb.useGravity = true;
    }

    IEnumerator delayVelocity()
    {
        yield return new WaitForSeconds(1);
        rb.velocity = Vector3.zero;
    }
}
