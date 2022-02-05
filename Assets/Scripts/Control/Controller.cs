
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    //Public Variables
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Pause = delegate { };
    public static Action Death = delegate { };
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

    private void Awake()
    {
        PlyCtrl = new PlayerControls();
        //this.gameObject.transform.position = GameAction.PlaceColOnGround(PlayerBrain.PB.plyCol);
        InitializeForm();
    }

    private void OnEnable()
    {
        PlyCtrl.Enable();
        InitializeForm();
    }

    //Called during OnEnable to change the form and stats of the player when it changes form
    void InitializeForm()
    {
        tag = form;
        PlayerBrain.PB.currentController = this;
        PlayerBrain.PB.plyCol.size = colliderSize;
        PlayerBrain.PB.plyCol.offset = colliderOffset;
        PlayerBrain.PB.plyCol.direction = direction;
        PlayerBrain.PB.plyCol.density = density;
        PlayerBrain.PB.plyAnim.runtimeAnimatorController = animatorController;
    }

    void OnDisable()
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
        PlyCtrl.Player.Interact.performed += _ => Interact();

        //Jump
        PlyCtrl.Player.Jump.performed += _ => Jump();

        //Embody
        PlyCtrl.Player.Embody.performed += _ => Embody();

        //Pause
        PlyCtrl.Player.Pause.performed += _ => Pause();

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
                    Mathf.Abs(transform.localScale.y),Mathf.Abs(transform.localScale.z));
                right = true;
                left = false;
            }
            else if (PlyCtrl.Player.Movement.ReadValue<float>() < 0 || PlyCtrl.Player.FishInWater.ReadValue<Vector2>().x < 0)
            {
                this.gameObject.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 
                    Mathf.Abs(transform.localScale.y),Mathf.Abs(transform.localScale.z));
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
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //If the Trigger is Death, tirgger Death
        if (other.CompareTag("Death") || other.CompareTag("Skeleton"))
        {
            PlayerBrain.PB.plyAnim.SetTrigger("Death");
        }
    }

    public virtual void OnTriggerStay2D(Collider2D other)
    {

    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {

    }

    public virtual void Jump()
    {
        PlayerBrain.PB.plyAnim.SetTrigger("takeOff");
        PlayerBrain.PB.plyAnim.SetBool("isJumping", true);
    }

    //Checks if the player is on the ground
    public bool isGrounded()
    {
        float dist = 0f;
        Vector2 origin = new Vector2(PlayerBrain.PB.plyCol.bounds.center.x, PlayerBrain.PB.plyCol.bounds.min.y);
        Vector2 size = new Vector2(PlayerBrain.PB.plyCol.size.x, 0.05f);
        RaycastHit2D hit = Physics2D.CapsuleCast(origin, size, CapsuleDirection2D.Horizontal, 0f, Vector2.down, 
            dist, PlayerBrain.PB.groundLayerMask);

        //Debug.Log(hit.collider);
        return hit.collider != null;
    }

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

    //Special cooldown
    IEnumerator SpecialCoolDown()
    {
        yield return new WaitForSeconds(cooldownTime);
        specialReady = true;
    }

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
        Death();
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
        float dist = 0.05f;
        RaycastHit2D hit = Physics2D.CapsuleCast(PlayerBrain.PB.plyCol.bounds.center, PlayerBrain.PB.plyCol.size, 
            PlayerBrain.PB.plyCol.direction, 0f, Vector2.down,dist, PlayerBrain.PB.groundLayerMask);

        Gizmos.DrawLine(hit.centroid + new Vector2(PlayerBrain.PB.plyCol.bounds.extents.x, 0),
            hit.centroid - new Vector2(PlayerBrain.PB.plyCol.bounds.extents.x, 0));
        Gizmos.DrawLine(hit.centroid + new Vector2(0, PlayerBrain.PB.plyCol.bounds.extents.y),
            hit.centroid - new Vector2(0, PlayerBrain.PB.plyCol.bounds.extents.y));
    }
}
