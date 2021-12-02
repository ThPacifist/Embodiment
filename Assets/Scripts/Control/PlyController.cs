using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlyController : MonoBehaviour
{
    //Assets and Public Variables
    PlayerControls PlyCtrl;
    SpecialInteractions SpcIntr;
    public Transform attackBox;
    public Rigidbody2D rb;
    public SpriteRenderer plySprite;
    public Animator plyAnim;
    public CapsuleCollider2D capCollider; 
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Special = delegate { };
    public static Action Pause = delegate { };
    public static Action Death = delegate { };
    public ControlMovement cntrlMove;
    public float speed;
    public float jumpHeight;
    public bool canMove = true;
    public bool canJump = true;
    PlayerInput test;

    [SerializeField]
    LayerMask groundLayerMask;

    //Private Variables
    public bool OnWall = false;
    bool batJump = true;
    bool delayGroundCheck;
    Vector2 catDir;
    AudioManager audioManager;

    bool right;
    bool left;

    public bool Right
    { get { return right; } }
    public bool Left
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
        audioManager = GameObject.FindObjectOfType<AudioManager>();
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
        if (canMove)
        {
            //Movement for Fish
            if (this.CompareTag("Fish"))
            {
                //Movement when in water
                if (inWater)
                {
                    //Move when the player is pressing buttons
                    if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>() != Vector2.zero)
                    {
                        if (Math.Abs(rb.velocity.x + rb.velocity.y) < speed)
                        {
                            rb.AddForce(Vector2.one * PlyCtrl.Player.FishInWater.ReadValue<Vector2>() * speed * 10);
                        }
                    }
                }
                //Movement when on the ground
                else if (isGrounded())
                {
                    //Move when the player is pressing buttons
                    if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                    {
                        if (Math.Abs(rb.velocity.x) < speed)
                        {
                            rb.AddForce(Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed * 0.3f);
                        }
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
                if (this.CompareTag("Cat") && OnWall)
                {
                    rb.gravityScale = 0;
                    rb.AddForce(catDir, ForceMode2D.Impulse);
                    if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>() != Vector2.zero)
                    {
                        //Checks if the player is pushing up or down
                        if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>().y != 0)
                        {
                            rb.velocity += (Vector2.up * PlyCtrl.Player.FishInWater.ReadValue<Vector2>().y * speed * 0.5f) - new Vector2(0, rb.velocity.y);
                            if (audioManager != null)
                            {
                                audioManager.Play("catClimb", true);
                            }
                        }
                        //Otherwise checks if they push left or right, meaning they can
                        else
                        {
                            if (audioManager != null)
                            {
                                //Debug.Log("Inside if");
                                audioManager.Stop("catClimb");
                            }
                            if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>().x != 0)
                            {
                                rb.velocity += (Vector2.right * PlyCtrl.Player.FishInWater.ReadValue<Vector2>().x * speed * 0.5f) - new Vector2(rb.velocity.x, 0);
                            }
                        }
                    }
                    else if (audioManager != null)
                    {
                        audioManager.Stop("catClimb");
                    }
                }
                //Regular grounded movement
                else if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                {
                    //Movement
                    if (Math.Abs(rb.velocity.x) < speed)
                    {
                        rb.AddForce(Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed * 10);
                    }
                    //Audio
                    if (audioManager != null)
                    {
                        audioManager.Stop("catClimb");
                        if (tag == "Blob")
                        {
                            audioManager.Play("blobStep", true);
                        }
                        else
                        {
                            //audioManager.Play("boneStep", true);
                        }
                    }
                }
                else
                {
                    if (audioManager != null)
                    {
                        audioManager.Stop("blobStep");
                    }
                }
            }
        }
        else if(spcInter.isAttached)
        {
            if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
            {
                rb.AddForce(Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * 0.6f, ForceMode2D.Impulse);
            }
        }
        //If Cat is not on a wall
        if(!OnWall)
        {
            rb.gravityScale = 1;
            if (audioManager != null)
            {
                audioManager.Stop("catClimb");
            }
        }

        if(!isGrounded())
        {
            if (audioManager != null)
            {
                audioManager.Stop("blobStep");
                //audioManager.Stop("boneStep");
            }
        }

        //Remove momentum while on ground
        if (PlyCtrl.Player.Movement.ReadValue<float>() == 0 && isGrounded())
        {
            //Reduce the player's speed by half
            rb.velocity *= new Vector2(0.75f, 1);
            //Change any held boxes velocity to match the player
            if (spcInter.heldBox != null)
            {
                spcInter.heldBox.velocity = rb.velocity;
            }
        }

        //Remove momentum while on wall
        if(PlyCtrl.Player.FishInWater.ReadValue<Vector2>().y == 0 && OnWall)
        {
            rb.velocity *= new Vector2(1, 0.5f);
        }


        #region Animation Block
        if (PlyCtrl.Player.Movement.ReadValue<float>() != 0 && canMove)
        {
            plyAnim.SetBool("Walking", true);
        }
        else
        {
            plyAnim.SetBool("Walking", false);
        }

        if (isGrounded())
        {
            plyAnim.SetBool("isJumping", false);
        }
        else
        {
            plyAnim.SetBool("isJumping", true);
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

        if (OnWall)
        {
            plyAnim.SetBool("Climb", true);

            if(PlyCtrl.Player.FishInWater.ReadValue<Vector2>().y != 0)
            {
                plyAnim.SetBool("Walking", true);
            }
            else
            {
                plyAnim.SetBool("Walking", false);
            }
        }
        else
        {
            plyAnim.SetBool("Climb", false);
        }

        if (!spcInter.HboxHeld)
        {
            //Set facing direction
            if (PlyCtrl.Player.Movement.ReadValue<float>() > 0)
            {
                this.gameObject.transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y),
                    Mathf.Abs(transform.localScale.z));
                right = true;
                left = false;
            }
            else if (PlyCtrl.Player.Movement.ReadValue<float>() < 0)
            {
                this.gameObject.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y),
                    Mathf.Abs(transform.localScale.z));
                left = true;
                right = false;
            }
            else
            {
                left = false;
                right = false;
            }
        }
        else
        {
            //Set to Pushing or Pulling
            if (this.transform.localScale.x > 0)//When player is facing left, while holding a heavy box
            {
                if (PlyCtrl.Player.Movement.ReadValue<float>() > 0)//When player is pressing right
                {
                    plyAnim.SetInteger("Facing", -1);
                }
                else if (PlyCtrl.Player.Movement.ReadValue<float>() < 0)//When player is pressing left
                {
                    plyAnim.SetInteger("Facing", 1);
                }
                else
                {
                    plyAnim.SetInteger("Facing", 0);
                }
            }
            else if (this.transform.localScale.x < 0)//When player is facing right, while holding a heavy box
            {
                if (PlyCtrl.Player.Movement.ReadValue<float>() > 0)//When player is pressing right
                {
                    plyAnim.SetInteger("Facing", 1);
                }
                else if (PlyCtrl.Player.Movement.ReadValue<float>() < 0)//When player is pressing left
                {
                    plyAnim.SetInteger("Facing", -1);
                }
                else
                {
                    plyAnim.SetInteger("Facing", 0);
                }
            }
        }

        //Set direction as cat on wall
        if(OnWall && catDir == Vector2.right)
        {
            this.gameObject.transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y),
                Mathf.Abs(transform.localScale.z));
            right = true;
            left = false;
        }
        else if(OnWall && catDir == Vector2.left)
        {
            this.gameObject.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y),
                Mathf.Abs(transform.localScale.z));
            left = true;
            right = false;
        }
        #endregion
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
        if (canJump)
        {
            //If time is moving, do something
            if (Time.timeScale > 0)
            {
                //Fly when bat
                if (CompareTag("Bat") && batJump)
                {
                    batJump = false;
                    rb.AddForce((Vector2.up * jumpHeight) - new Vector2(0, rb.velocity.y), ForceMode2D.Impulse);
                    plyAnim.SetTrigger("Flap");
                    if (audioManager != null)
                    {
                        audioManager.Play("wingFlap", true);
                    }
                    StartCoroutine(FlyCoolDown());
                }
                //Side jump when climbing
                else if (CompareTag("Cat") && OnWall)
                {
                    rb.AddForce((-catDir * 25) - new Vector2(rb.velocity.x, 0), ForceMode2D.Impulse);
                    catDir = -catDir;
                }
                //Regular jump when appropriate
                else
                {
                    if (isGrounded() || inWater)
                    {
                        rb.AddForce((Vector2.up * jumpHeight) - new Vector2(0, rb.velocity.y), ForceMode2D.Impulse);

                    }
                    else if (spcInter.isAttached)
                    {
                        spcInter.ShootTendril();
                        rb.AddForce((rb.velocity * 2) - rb.velocity, ForceMode2D.Impulse);
                    }
                }
                if (!delayGroundCheck)
                {
                    plyAnim.SetTrigger("takeOff");
                }
                plyAnim.SetBool("isJumping", true);
                DelayGroundCheck();
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

    //Check when a trigger is entered
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("Fish"))
        {
            if (other.CompareTag("Water"))
            {
                if (audioManager != null)
                {
                    audioManager.Play("splash", true);
                }
                inWater = true;
            }
        }
        else if (CompareTag("Blob"))
        {
            if (other.CompareTag("Water"))
            {
                //Makes Blob float
                if (audioManager != null)
                {
                    audioManager.Play("splash", true);
                }
                inWater = true;
            }
        }
        else
        {
            if (other.CompareTag("Water"))
            {
                Death();
            }
        }

        //If the Trigger is Death, call Death delegate
        if(other.CompareTag("Death") || other.CompareTag("Skeleton"))
        {
            Death();
        }
    }

    //Check for when the player stays in a trigger
    private void OnTriggerStay2D(Collider2D other)
    {
        if (CompareTag("Fish"))
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
        if (CompareTag("Fish"))
        {
            if (other.CompareTag("Water"))
            {
                inWater = false;
            }
        }
        else if (CompareTag("Blob"))
        {
            if (other.CompareTag("Water"))
            {
                //Blob jumps out of water
                inWater = false;
            }
        }
    }

    //Kill from menu
    public void MenuKill()
    {
        Debug.Log("Menu Kill called");
        Death();
    }

    public void SetCatOnWall(bool value, Vector2 direction) 
    { 
        OnWall = value;
        catDir = direction;
    }

    //Checks if the player is on the ground
    public bool isGrounded()
    {
        float dist = 0.05f;
        RaycastHit2D hit = Physics2D.CapsuleCast(capCollider.bounds.center, capCollider.size, capCollider.direction, 0f, Vector2.down, 
            dist, groundLayerMask);

        if (!delayGroundCheck)
        {
            //Debug.Log(hit.collider);
            return hit.collider != null;
        }
        else
            return false;
    }

    /* Bat */
    //Cooldown for jumping in midair
    IEnumerator FlyCoolDown()
    {
        yield return new WaitForSeconds(0.1f);
        batJump = true;
    }

    void DelayGroundCheck()
    {
        StartCoroutine(DelayGroundCheckIE());
    }

    IEnumerator DelayGroundCheckIE()
    {
        delayGroundCheck = true;
        yield return new WaitForSeconds(0.5f);
        delayGroundCheck = false;
    }

    public void DisableMovement()
    {
        canMove = false;
        canJump = false;
    }

    public void RenableMovement()
    {
        canMove = true;
        canJump = true;
    }

    public void PlaySoundFromAudioManager(string name)
    {
        audioManager.PlayAnyway(name);
    }

    //Used for bug testing
    private void OnDrawGizmos()
    {
        float dist = 0.05f;
        RaycastHit2D hit = Physics2D.CapsuleCast(capCollider.bounds.center, capCollider.size, capCollider.direction, 0f, Vector2.down,
            dist, groundLayerMask);

        Gizmos.DrawLine(hit.centroid + new Vector2(capCollider.bounds.extents.x, 0), 
            hit.centroid - new Vector2(capCollider.bounds.extents.x, 0));
        Gizmos.DrawLine(hit.centroid + new Vector2(0, capCollider.bounds.extents.y), 
            hit.centroid - new Vector2(0, capCollider.bounds.extents.y));
    }
}
