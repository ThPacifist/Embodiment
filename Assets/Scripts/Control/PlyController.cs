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
    public CapsuleCollider2D capCollider; 
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Special = delegate { };
    public static Action Pause = delegate { };
    public float speed;
    public float jumpHeight;
    public bool move = true;

    [SerializeField]
    LayerMask groundLayerMask;

    //Private Variables
    private bool catClimb = false;
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
        if (move)
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
                }
                //Movement when on the ground
                else if (isGrounded())
                {
                    //Move when the player is pressing buttons
                    if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                    {
                        rb.velocity += (Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed * 0.3f) - new Vector2(rb.velocity.x, 0);
                    }
                }
                else
                {
                    //Move when the player is pressing the direction
                    if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                    {
                        rb.velocity += (Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed * 0.5f) - new Vector2(rb.velocity.x, 0);
                    }
                }
            }
            else
            {
                //Cat climb movement
                if (player.CompareTag("Cat") && catClimb)
                {
                    if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>() != Vector2.zero)
                    {
                        rb.velocity += (new Vector2(1, 1) * PlyCtrl.Player.FishInWater.ReadValue<Vector2>() * speed * 0.5f) - new Vector2(rb.velocity.x, rb.velocity.y);
                        //Stop leteral movement and gravity while moving up
                        if ((PlyCtrl.Player.FishInWater.ReadValue<Vector2>().y > 0) && PlyCtrl.Player.Jump.ReadValue<float>() == 0)
                        {
                            rb.velocity = new Vector2(0, rb.velocity.y);
                            rb.gravityScale = 0;
                        }
                        //Keep gravity when moving in other directions
                        else if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>().y > 0 || PlyCtrl.Player.FishInWater.ReadValue<Vector2>().x != 0)
                        {
                            rb.gravityScale = 1;
                        }
                    }
                }
                //Regular grounded movement
                else if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                {
                    rb.velocity += (Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed) - new Vector2(rb.velocity.x, 0);
                }

            }
        }

        //Set facing direction
        if (PlyCtrl.Player.Movement.ReadValue<float>() > 0)
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            right = true;
            left = false;
        }
        else if (PlyCtrl.Player.Movement.ReadValue<float>() < 0)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            left = true;
            right = false;
        }

        //Remove momentum while on ground
        if (PlyCtrl.Player.Movement.ReadValue<float>() == 0 && isGrounded())
        {
            rb.velocity *= new Vector2(0.5f, 1);
        }

        //Check if the blob is attached
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
                Debug.Log("Jump");
                rb.AddForce((catDir * 25) - new Vector2(rb.velocity.x, 0), ForceMode2D.Impulse);
            }
            //Regular jump when appropriate
            else
            {
                if (isGrounded() || inWater || spcInter.isAttached)
                {
                    if (spcInter.isAttached)
                    {
                        spcInter.ShootTentril();
                    }
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
        /*if(!collision.gameObject.CompareTag("Wall"))
        {
            if (!inWater)
            {
                isGrounded = true;
            }
        }*/
    }

    //Check for when a collision is exited
    private void OnCollisionExit2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("Wall"))
        {

        }
        else if(collision.gameObject.CompareTag("Ground") && !inWater)
        {
            isGrounded = false;
        }*/
    }

    //Check when a trigger is entered
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered " + other.tag);
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
                catClimb = true;
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
    }

    //Check for when the trigger is exited
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exited " + other.tag);
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
                catClimb = false;
                rb.gravityScale = 1;
            }
        }
    }

    //Checks if the player is on the ground
    bool isGrounded()
    {
        float dist = 0.4f;
        RaycastHit2D hit = Physics2D.CapsuleCast(capCollider.bounds.center, capCollider.size, capCollider.direction, 0f, Vector2.down, 
            dist, groundLayerMask);

        //Bug Fixing Code: Do Not Delete
        /*if (hit.collider != null)
        {
            Debug.DrawLine(hit.centroid + new Vector2(capCollider.bounds.extents.x, 0), hit.centroid - new Vector2(capCollider.bounds.extents.x, 0));
            Debug.DrawLine(hit.centroid + new Vector2 (0, capCollider.bounds.extents.y), hit.centroid - new Vector2(0, capCollider.bounds.extents.y));
            Time.timeScale = 0;
        }*/

        Debug.Log(hit.collider);
        return hit.collider != null;
    }
    //Use this version to differentiate what type of jumpable it is
    bool isGrounded(string tag)
    {
        float extraDist = 0.1f;
        RaycastHit2D hit = Physics2D.CapsuleCast(capCollider.bounds.center, capCollider.size, capCollider.direction, 0f, Vector2.down,
            capCollider.bounds.extents.y + extraDist, groundLayerMask);

        Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            if(hit.collider.CompareTag(tag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
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
