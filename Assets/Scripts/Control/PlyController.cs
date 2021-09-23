using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlyController : MonoBehaviour
{
    //Assets and Public Variables
    PlayerControls PlyCtrl;
    public Transform player;
    public Transform attackBox;
    public Rigidbody2D rb;
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Special = delegate { };
    public static Action Pause = delegate { };
    public float speed;
    public float jumpHeight;

    public float velocityX;
    public float velocityY;

    //Private Variables
    private bool catClimb = false;
    public bool isGrounded = false;
    private bool pushing = false;
    private bool canJump = true;

    [SerializeField]
    bool inWater = false;
    [SerializeField]
    bool aboveWater = false;

    private void Awake()
    {
        PlyCtrl = new PlayerControls();
    }
    private void OnEnable()
    {
        PlyCtrl.Enable();
    }

    void OnDisable()
    {
        PlyCtrl.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        velocityX = rb.velocity.x;
        velocityY = rb.velocity.y;

        //Activates Special
        PlyCtrl.Player.Special.performed += _ => Special();

        //Activates Interact
        PlyCtrl.Player.Interact.performed += _ => Interact();

        ///Movement for Fish
        if (player.CompareTag("Fish"))
        {
            if (inWater)
            {
                if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>() != Vector2.zero)
                {
                    rb.velocity += (Vector2.one * PlyCtrl.Player.FishInWater.ReadValue<Vector2>() * speed) - new Vector2(rb.velocity.x, rb.velocity.y);
                }
            }
            else if (isGrounded)
            {
                if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                {
                    rb.velocity += (Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed * 0.3f) - new Vector2(rb.velocity.x, 0);
                }
            }
            else
            {
                if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                {
                    rb.velocity += (Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed * 0.5f) - new Vector2(rb.velocity.x, 0);
                }
            }
        }
        else
        {
            //Movement
            if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
            {
                rb.velocity += (Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed) - new Vector2(rb.velocity.x, 0);
                if(rb.velocity.x > 0)
                {

                }
                else if(rb.velocity.x < 0)
                {

                }
            }
        }
        //Jump
        PlyCtrl.Player.Jump.performed += _ => Jump();

        //Embody
        PlyCtrl.Player.Embody.performed += _ => Embody();

        //Pause
        PlyCtrl.Player.Pause.performed += _ => Pause();
    }

    private void Jump()
    {
        if (player.CompareTag("Bat"))
        {
            rb.AddForce((Vector2.up * jumpHeight) - new Vector2(0, rb.velocity.y), ForceMode2D.Impulse);
        }
        else
        {
            if (isGrounded)
            {
                isGrounded = false;
                //catClimb = false;
                rb.gravityScale = 1;
                rb.AddForce((Vector2.up * jumpHeight) - new Vector2(0, rb.velocity.y), ForceMode2D.Impulse);
            }
        }
    }

    //Check if they're on the ground
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && !inWater)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player.CompareTag("Fish"))
        {
            if (other.CompareTag("Water"))
            {
                inWater = true;
                rb.gravityScale = 0.1f;
                //aboveWater = false;
            }
        }
        else if (player.CompareTag("Blob"))
        {
            if (other.CompareTag("Water"))
            {
                //Makes Blob float
            }
        }
        else
        {
            if (other.CompareTag("Water"))
            {
                player.gameObject.SetActive(false);
            }
        }

        //If the Trigger is Death, kill the player
        if(other.CompareTag("Death"))
        {
            player.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (player.CompareTag("Fish"))
        {
            if (other.CompareTag("Water"))
            {
                inWater = true;
            }
        }
        else if (player.CompareTag("Blob"))
        {
            if (other.CompareTag("Water"))
            {
                //Keep Blob afloat
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (player.CompareTag("Fish"))
        {
            if (other.CompareTag("Water"))
            {
                inWater = false;
                rb.gravityScale = 1;
                //aboveWater = true;
            }
        }
        else if (player.CompareTag("Blob"))
        {
            if (other.CompareTag("Water"))
            {
                //Blob jumps out of water
            }
        }
    }

    /* Bat */
    //Cooldown for jumping in midair
    IEnumerator FlyCoolDown()
    {
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }

    /* Cat */
    //When the cat starts climbing, things need to happen here
    private void Climb()
    {
        if (catClimb)
        {
            catClimb = false;
            rb.gravityScale = 1;
        }
        else
        {
            catClimb = true;
            rb.gravityScale = 0;
        }
    }

    //Delays the activation of gravity to give illusion of jumping out of the water
    IEnumerator delayGravity()
    {
        yield return new WaitForSeconds(2);
        rb.gravityScale = 1;
    }
    //Delays the reduce of velocity to give the illusion of friction when jumping in water
    IEnumerator SimulateFriction()
    {
        yield return new WaitForSeconds(1);
        rb.velocity = Vector2.zero;
    }
}
