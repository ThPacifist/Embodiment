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
    public SpriteRenderer plySprite;
    public Animator plyAnim;
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Special = delegate { };
    public static Action Pause = delegate { };
    public float speed;
    public float jumpHeight;

    //Private Variables
    private bool catClimb = false;
    public bool isGrounded = false;
    private bool canJump = true;
    Vector2 catDir;

    static bool right;
    static bool left;

    public static bool Right
    { get { return right; } }
    public static bool Left
    { get { return left; } }

    [SerializeField]
    bool inWater = false;

    public bool InWater
    { get { return inWater; } }

    [SerializeField]
    SpecialInteractions spcInter;

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

    //Start is called at the start of this script
    private void Start()
    {
        //Spceial Interact
        PlyCtrl.Player.Special.performed += _ => SpecialS();

        //Regular interact
        PlyCtrl.Player.Interact.performed += _ => InteractI();

        //Jump
        PlyCtrl.Player.Jump.performed += _ => Jump();

        //Embody
        PlyCtrl.Player.Embody.performed += _ => EmbodyE();

        //Pause
        PlyCtrl.Player.Pause.performed += _ => Pause();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //Movement for Fish
        if (player.CompareTag("Fish"))
        {
            //Movement when in water
            if (inWater)
            {
                //Move when the player is pressing buttons
                if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>() != Vector2.zero)
                {
                    rb.velocity += (Vector2.one * PlyCtrl.Player.FishInWater.ReadValue<Vector2>() * speed) - new Vector2(rb.velocity.x, rb.velocity.y);
                }
                //Set facing direction
                if (PlyCtrl.Player.Movement.ReadValue<float>() > 0)
                {
                    plySprite.flipX = true;
                    right = true;
                    left = false;
                }
                else if (PlyCtrl.Player.Movement.ReadValue<float>() < 0)
                {
                    plySprite.flipX = false;
                    left = true;
                    right = false;
                }
            }
            //Movement when on the ground
            else if (isGrounded)
            {
                //Move when the player is pressing buttons
                if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                {
                    rb.velocity += (Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed * 0.3f) - new Vector2(rb.velocity.x, 0);
                }
                //Set facing direction
                if(PlyCtrl.Player.Movement.ReadValue<float>() > 0)
                {
                    plySprite.flipX = true;
                    right = true;
                    left = false;
                }
                else if (PlyCtrl.Player.Movement.ReadValue<float>() < 0)
                {
                    plySprite.flipX = false;
                    left = true;
                    right = false;
                }
            }
            else
            {
                //Move when the player is pressing the direction
                if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                {
                    rb.velocity += (Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed * 0.5f) - new Vector2(rb.velocity.x, 0);
                }
                //Set facing direction
                if(PlyCtrl.Player.Movement.ReadValue<float>() > 0)
                {
                    plySprite.flipX = true;
                    right = true;
                    left = false;
                }
                else if (PlyCtrl.Player.Movement.ReadValue<float>() < 0)
                {
                    plySprite.flipX = false;
                    left = true;
                    right = false;
                }
            }
        }
        else
        {
            //Cat climb movement
            if(player.CompareTag("Cat") && catClimb)
            {
                if(PlyCtrl.Player.FishInWater.ReadValue<Vector2>().y > 0 || PlyCtrl.Player.FishInWater.ReadValue<Vector2>().y < 0)
                {
                    rb.velocity += (Vector2.up * PlyCtrl.Player.FishInWater.ReadValue<Vector2>() * speed * 0.5f) - new Vector2(0, rb.velocity.y);
                }
            }
            //Regular grounded movement
            else if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
            {
                rb.velocity += (Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed) - new Vector2(rb.velocity.x, 0);
            }
            //Set facing direction
            if (PlyCtrl.Player.Movement.ReadValue<float>() > 0)
            {
                plySprite.flipX = true;
                right = true;
                left = false;
            }
            else if (PlyCtrl.Player.Movement.ReadValue<float>() < 0)
            {
                plySprite.flipX = false;
                left = true;
                right = false;
            }
        }

        if (spcInter.isAttached)
        {
            plyAnim.SetBool("Swing", true);
        }
        else
        {
            plyAnim.SetBool("Swing", false);
        }
    }

    //Special check
    private void SpecialS()
    {
        if(Time.timeScale > 0)
        {
            Special();
        }
    }

    //Interact check
    private void InteractI()
    {
        if (Time.timeScale > 0)
        {
            Interact();
        }
    }

    //Jump
    private void Jump()
    {
        //If time is moving, do something
        if (Time.timeScale > 0)
        {
            //Fly when bat
            if (player.CompareTag("Bat") && canJump)
            {
                canJump = false;
                rb.AddForce((Vector2.up * jumpHeight) - new Vector2(0, rb.velocity.y), ForceMode2D.Impulse);
                StartCoroutine(FlyCoolDown());
            }
            //Side jump when climbing
            else if (player.CompareTag("Cat") && catClimb)
            {
                rb.AddForce((catDir * jumpHeight) - new Vector2(rb.velocity.x, 0), ForceMode2D.Impulse);
            }
            //Regular jump when appropriate
            else
            {
                if (isGrounded || inWater || spcInter.isAttached)
                {
                    if (spcInter.isAttached)
                    {
                        spcInter.ShootTentril();
                    }
                    isGrounded = false;
                    rb.AddForce((Vector2.up * jumpHeight) - new Vector2(0, rb.velocity.y), ForceMode2D.Impulse);
                }
            }
        }
    }

    //Embody check
    private void EmbodyE()
    {
        if (Time.timeScale > 0)
        {
            Embody();
        }
    }

    //Check if they're on the ground
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Wall"))
        {
            if (!inWater)
            {
                isGrounded = true;
            }
        }
    }

    //Check for when a collision is exited
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {

        }
        else if(collision.gameObject.CompareTag("Ground") && !inWater)
        {
            isGrounded = false;
        }
    }

    //Check when a trigger is entered
    private void OnTriggerEnter2D(Collider2D other)
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
                //Makes Blob float
                inWater = true;
            }
        }
        else if(player.CompareTag("Cat"))
        {
            if(other.CompareTag("Climb"))
            {
                Climb();
                if(rb.velocity.x > 0)
                {
                    catDir = Vector2.left;
                }
                else
                {
                    catDir = Vector2.right;
                }
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

    //Check for when the player stays in a trigger
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

    //Check for when the trigger is exited
    private void OnTriggerExit2D(Collider2D other)
    {
        if (player.CompareTag("Fish"))
        {
            if (other.CompareTag("Water"))
            {
                inWater = false;
                rb.gravityScale = 1;
            }
        }
        else if (player.CompareTag("Blob"))
        {
            if (other.CompareTag("Water"))
            {
                //Blob jumps out of water
                inWater = false;
                rb.gravityScale = 1;
            }
        }
        else if (player.CompareTag("Cat"))
        {
            if (other.CompareTag("Climb"))
            {
                Climb();
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
}
