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
    public Rigidbody2D rb;
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Special = delegate { };
    public static Action Pause = delegate { };
    public float speed = 10;
    public float jumpHeight = 2;
    public float velocityX;
    public float velocityY;
    //Private variables


    //Things to do on awake
    private void Awake()
    {
        input = new WaterControls();
        rb = this.GetComponent<Rigidbody2D>();

        if(inWater)
        {
            rb.gravityScale = 0;
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
        Vector2 a = input.WaterMovement.Movement.ReadValue<Vector2>() * speed * Time.deltaTime;
        velocityX = rb.velocity.x;
        velocityY = rb.velocity.y;

       //Movement 
        if (input.WaterMovement.Movement.ReadValue<Vector2>() != Vector2.zero)
        {
            rb.velocity += (Vector2.right * input.WaterMovement.Movement.ReadValue<Vector2>() * speed) - new Vector2(rb.velocity.x, 0);
        }

    //If not in water don't allow vertical movement
        if (!inWater)
        {
            //rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        //If on land slow down movement
        if(onLand)
        {
            rb.velocity *= 0.5f;
        }

        //Jump
        if (input.WaterMovement.Jump.ReadValue<float>() > 0 && onLand)
        {
            rb.AddForce((Vector2.up * jumpHeight) - rb.velocity, ForceMode2D.Impulse);
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = true;
            onLand = false;
            aboveWater = false;
            rb.gravityScale = 0;
            StartCoroutine(delayVelocity());
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;
            aboveWater = true;
            StartCoroutine(delayGravity());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (aboveWater)
        {
            onLand = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        onLand = false;
    }

    //Delays the activation of gravity to give illusion of jumping out of the water
    IEnumerator delayGravity()
    {
        yield return new WaitForSeconds(2);
        rb.gravityScale = 1;
    }
    //Delays the reduce of velocity to give the illusion of friction when jumping in water
    IEnumerator delayVelocity()
    {
        yield return new WaitForSeconds(1);
        rb.velocity = Vector2.zero;
    }
}
