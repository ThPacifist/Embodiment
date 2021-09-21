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
        //Activates Special
        if(PlyCtrl.Player.Special.ReadValue<float>() > 0)
        {
            Special();
        }

        //Activates Interact
        if (PlyCtrl.Player.Interact.ReadValue<float>() > 0)
        {
            Interact();
        }
        ///Movement for Fish
        if (player.CompareTag("Fish"))
        {
            if (inWater)
            {
                if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>() != Vector2.zero)
                {
                    rb.velocity += (Vector2.right * PlyCtrl.Player.Movement.ReadValue<Vector2>() * speed) - new Vector2(rb.velocity.x, 0);
                }
            }
            else if (isGrounded)
            {
                if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                {
                    rb.velocity *= 0.5f;
                    rb.velocity += (Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed) - new Vector2(rb.velocity.x, 0);
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
        if (player.CompareTag("Bat"))
        {
            if (PlyCtrl.Player.Jump.ReadValue<float>() > 0 && canJump)
            {
                rb.AddForce((Vector2.up * jumpHeight) - new Vector2(0, rb.velocity.y), ForceMode2D.Impulse);
                canJump = false;
                StartCoroutine(FlyCoolDown());
            }
        }
        else
        {
            if (PlyCtrl.Player.Jump.ReadValue<float>() > 0 && isGrounded)
            {
                isGrounded = false;
                //catClimb = false;
                rb.gravityScale = 1;
                rb.AddForce((Vector2.up * jumpHeight) - new Vector2(0, rb.velocity.y), ForceMode2D.Impulse);
            }
        }

        //Embody
        if (PlyCtrl.Player.Embody.ReadValue<float>() > 0)
        {
            Embody();
        }

        //Pause
        if (PlyCtrl.Player.Pause.ReadValue<float>() > 0)
        {
            Pause();
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
                aboveWater = false;
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
            //All other forms sink and die
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
                aboveWater = true;
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
    IEnumerator delayVelocity()
    {
        yield return new WaitForSeconds(1);
        rb.velocity = Vector2.zero;
    }
}
