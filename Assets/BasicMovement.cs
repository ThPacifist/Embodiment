using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMovement : MonoBehaviour
{
    public enum Direction
    {
        Right,
        Left
    }

    PlayerControls plyCntrl;

    public static Action<Vector2> JumpAction = delegate { };

    public float speed = 5.0f;
    public float jumpForce = 2.0f;
    public Direction direction = Direction.Left;
    public Rigidbody2D rb;
    public CapsuleCollider2D CapCollider;
    public TentacleManager tentacleManager;
    public bool isGrounded = false;

    private void OnEnable()
    {
        plyCntrl = new PlayerControls();
        plyCntrl.Enable();
    }

    private void OnDisable()
    {
        plyCntrl.Disable();
    }

    private void Awake()
    {
        tentacleManager = TentacleManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        plyCntrl.Player.Jump.performed += _ => Jump();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckIfGrounded();

        if(plyCntrl.Player.Movement.ReadValue<float>() != 0)
        {
            rb.AddForce(Vector2.right * plyCntrl.Player.Movement.ReadValue<float>() * speed);
            if(plyCntrl.Player.Movement.ReadValue<float>() < 0)
            {
                direction = Direction.Left;
            }
            else
            {
                direction = Direction.Right;
            }
        }
    }

    //Checks if the player is on the ground
    public bool CheckIfGrounded()
    {
        float dist = 0f;
        int layer = LayerMask.GetMask("Jumpables", "PickupAbles");

        Vector2 origin = new Vector2(CapCollider.bounds.center.x, CapCollider.bounds.min.y);
        Vector2 size = new Vector2(CapCollider.size.x, 0.05f);
        RaycastHit2D hit = Physics2D.CapsuleCast(origin, size, CapsuleDirection2D.Horizontal, 0f, Vector2.down,
            dist, layer);

        //Debug.Log(hit.collider);
        return tentacleManager.CheckIfGrounded();
    }

    void Jump()
    {
        Vector2 force = Vector2.up * jumpForce;
        JumpAction(force);

        if(isGrounded)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
