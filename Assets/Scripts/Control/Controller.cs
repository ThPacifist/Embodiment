
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Base class for all player controllers
/// </summary>
public class Controller : MonoBehaviour
{
    /*  Things to Note:
     *  When we want to change the player to a form, makes sure to:
     *      Step 1: Disable the current controller from Player brain
     *      Step 2: Enable the controller of the target from
     *      
     *  The Enabling of a controller will set the values of the collider, set itself as the current controller, and everything else
     *  that need to be set for that particular form
     */


    //Public Variables
    [Header("Form Settings")]
    public string form;
    public float speed;
    public float jumpHeight;
    public float density = 1;
    public Vector2 colliderSize;
    public Vector2 colliderOffset;
    public CapsuleDirection2D direction;
    public RuntimeAnimatorController animatorController;

    //Protected Variables
    protected PlayerControls PlyCtrl;
    protected AudioManager audioManager;
    protected bool right;
    protected bool left;
    protected bool specialReady = true;
    protected float cooldownTime;

    //Private
    public bool Right
    { get { return right; } }
    public bool Left
    { get { return left; } }

    protected virtual void Awake()
    {
        PlyCtrl = new PlayerControls();
        audioManager = FindObjectOfType<AudioManager>();
        //this.gameObject.transform.position = GameAction.PlaceColOnGround(PlayerBrain.PB.plyCol);
        InitializeForm();
        TransitionController.slideInAction = UnFreezePlayer;
    }

    protected virtual void OnEnable()
    {
        PlyCtrl.Enable();
        InitializeForm();
    }

    //Called during OnEnable to change the form and stats of the player when it changes form
    protected virtual void InitializeForm()
    {
        tag = form;
        PlayerBrain.PB.currentController = this;
        PlayerBrain.PB.plyCol.size = colliderSize;
        PlayerBrain.PB.plyCol.offset = colliderOffset;
        PlayerBrain.PB.plyCol.direction = direction;
        PlayerBrain.PB.plyCol.density = density;
        PlayerBrain.PB.plyAnim.runtimeAnimatorController = animatorController;
    }

    protected virtual void OnDisable()
    {
        PlyCtrl.Disable();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        //Special Interact
        PlyCtrl.Player.Special.performed += _ => Special();

        //Regular interact
        PlyCtrl.Player.Interact.performed += _ => PlayerBrain.Interact();

        //Jump
        PlyCtrl.Player.Jump.performed += _ => Jump();

        //Embody
        PlyCtrl.Player.Embody.performed += _ => Embody();

        //Pause
        PlyCtrl.Player.Pause.performed += _ => PlayerBrain.Pause();

        PlayerBrain.PB.spring.enabled = false;
        PlayerBrain.PB.fixedJ.enabled = false;
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if (PlayerBrain.PB.canMove)
        {
            //Keeps track of what direction player is moving in and flips the player based on the direction they are heading in
            if (PlyCtrl.Player.Movement.ReadValue<float>() > 0 || PlyCtrl.Player.FishInWater.ReadValue<Vector2>().x > 0)
            {
                this.gameObject.transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x),
                    Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
                right = true;
                left = false;
            }
            else if (PlyCtrl.Player.Movement.ReadValue<float>() < 0 || PlyCtrl.Player.FishInWater.ReadValue<Vector2>().x < 0)
            {
                this.gameObject.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),
                    Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
                left = true;
                right = false;
            }
            else
            {
                left = false;
                right = false;
            }

            //Remove momentum while on ground
            if (PlyCtrl.Player.Movement.ReadValue<float>() == 0 && isGrounded())
            {
                //Reduce the player's speed by half
                PlayerBrain.PB.rb.velocity *= new Vector2(0.75f, 1);
            }

            if (PlyCtrl.Player.Movement.ReadValue<float>() != 0 && PlayerBrain.PB.canMove)
            {
                PlayerBrain.PB.plyAnim.SetBool("Walking", true);
            }
            else
            {
                PlayerBrain.PB.plyAnim.SetBool("Walking", false);
            }

            if (isGrounded())
            {
                PlayerBrain.PB.plyAnim.SetBool("isJumping", false);
            }
            else
            {
                PlayerBrain.PB.plyAnim.SetBool("isJumping", true);
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //If the Trigger is Death, trigger Death
        if (other.CompareTag("Death") || other.CompareTag("Skeleton"))
        {
            PlayerBrain.PB.plyAnim.SetTrigger("Death");
            SetToDefault();
        }
    }

    public virtual void OnTriggerStay2D(Collider2D other)
    {

    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {

    }

    /// <summary>
    /// Used in Player Data to reset all necessary values back to null, false, etc
    /// </summary>
    public virtual void SetToDefault()
    {

    }

    //Use base.Jump at the end of all overrides of Jump
    public virtual void Jump()
    {
        PlayerBrain.PB.plyAnim.SetTrigger("takeOff");
        PlayerBrain.PB.plyAnim.SetBool("isJumping", true);
    }

    //Checks if the player is on the ground
    public bool isGrounded()
    {
        float dist = 0f;
        int layer = LayerMask.GetMask("Jumpables", "PickupAbles");

        Vector2 origin = new Vector2(PlayerBrain.PB.plyCol.bounds.center.x, PlayerBrain.PB.plyCol.bounds.min.y);
        Vector2 size = new Vector2(PlayerBrain.PB.plyCol.size.x, 0.05f);
        RaycastHit2D hit = Physics2D.CapsuleCast(origin, size, CapsuleDirection2D.Horizontal, 0f, Vector2.down,
            dist, layer);

        //Debug.Log(hit.collider);
        return hit.collider != null;
    }

    //Pass in a valid string to play a sound
    public void PlaySoundFromAudioManager(string name)
    {
        audioManager.Play(name);
    }

    public virtual void Special()
    {

    }

    public virtual void CallFromAnimation(int value)
    {

    }

    /*//Special cooldown
    public IEnumerator SpecialCoolDown()
    {
        yield return new WaitForSeconds(cooldownTime);
        specialReady = true;
    }*/

    public void DisableMovement()
    {
        PlayerBrain.PB.canMove = false;
        PlayerBrain.PB.canJump = false;
    }

    public void RenableMovement()
    {
        PlayerBrain.PB.canMove = true;
        PlayerBrain.PB.canJump = true;
    }

    //Calls all functions subscribed to death; Used in Death animation
    void TriggerDeath()
    {
        PlayerBrain.Death();
        PlayerBrain.PB.plySpr.enabled = false;
    }

    void FreezePlayer()
    {
        PlayerBrain.PB.rb.simulated = false;
        PlayerBrain.PB.rb.velocity = Vector2.zero;
        DisableMovement();
    }

    void UnFreezePlayer()
    {
        PlayerBrain.PB.rb.simulated = true;
        RenableMovement();
    }

    //Used for bug testing
    private void OnDrawGizmos()
    {

    }

    //Checks for embodiment and causes it
    public void Embody()
    {
        if (!PlayerBrain.PB.inWater)
        {
            PlayerBrain.Embody();
        }
    }
}
