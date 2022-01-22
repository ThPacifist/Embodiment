
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    //Public Variables
    public ControlMovement cntrlMove;
    public SpecialInteractions spcInter;
    public Rigidbody2D rb;
    public Animator plyAnim;
    public CapsuleCollider2D capCollider;
    public static Action Interact = delegate { };
    public static Action Embody = delegate { };
    public static Action Special = delegate { };
    public static Action Pause = delegate { };
    public static Action Death = delegate { };
    public float speed;
    public float jumpHeight;
    public bool canMove = true;
    public bool canJump = true;

    //Private Variables
    protected PlayerControls PlyCtrl;
    protected AudioManager audioManager;
    protected bool right;
    protected bool left;
    
    //Private but Editable in Inspector
    [SerializeField]
    protected bool inWater = false;
    [SerializeField]
    LayerMask groundLayerMask;

    public bool Right
    { get { return right; } }
    public bool Left
    { get { return left; } }

    public bool InWater
    { get { return inWater; } }

    private void Awake()
    {
        PlyCtrl = new PlayerControls();
        this.gameObject.transform.position = GameAction.PlaceColOnGround(capCollider);
    }

    private void OnEnable()
    {
        PlyCtrl.Enable();
    }

    void OnDisable()
    {
        PlyCtrl.Disable();
    }

    // Start is called before the first frame update
    void Start()
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
    }

    // Update is called once per frame
    public virtual void LateUpdate()
    {
        //Remove momentum while on ground
        if (PlyCtrl.Player.Movement.ReadValue<float>() == 0 && isGrounded())
        {
            //Reduce the player's speed by half
            rb.velocity *= new Vector2(0.75f, 1);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //If the Trigger is Death, tirgger Death
        if (other.CompareTag("Death") || other.CompareTag("Skeleton"))
        {
            plyAnim.SetTrigger("Death");
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
        plyAnim.SetTrigger("takeOff");
        plyAnim.SetBool("isJumping", true);
    }

    //Checks if the player is on the ground
    public bool isGrounded()
    {
        float dist = 0.05f;
        RaycastHit2D hit = Physics2D.CapsuleCast(capCollider.bounds.center, capCollider.size, capCollider.direction, 0f, Vector2.down,
            dist, groundLayerMask);

        //Debug.Log(hit.collider);
        return hit.collider != null;
    }

    public void PlaySoundFromAudioManager(string name)
    {
        audioManager.Play(name);
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

    //Calls all functions subscribed to death; Used in Death animation
    void TriggerDeath()
    {
        Death();
        this.gameObject.SetActive(false);
    }

    void FreezePlayer()
    {
        rb.simulated = false;
        rb.velocity = Vector2.zero;
        DisableMovement();
    }

    void UnFreezePlayer()
    {
        rb.simulated = true;
        RenableMovement();
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
