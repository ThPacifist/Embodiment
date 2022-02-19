using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatController : Controller
{
    public static Action Scratch = delegate { };
    [Header("Cat Settings")]
    public bool OnWall;
    [HideInInspector]
    public bool treadmill = false;

    Vector2 catDir;

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(OnWall || !isGrounded())
        {
            Embodiment.canDisembody = false;
        }
        else
        {
            Embodiment.canDisembody = true;
        }

        if (PlayerBrain.PB.canMove && !treadmill)
        {
            //Regular grounded movement
            if (PlyCtrl.Player.Movement.ReadValue<float>() != 0 && !OnWall)
            {
                //Movement
                if (Mathf.Abs(PlayerBrain.PB.rb.velocity.x) < speed)
                {
                    PlayerBrain.PB.rb.AddForce(Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * 20 * PlayerBrain.PB.rb.mass);
                }

                //Audio
                if (audioManager != null)
                {
                    audioManager.Stop("catClimb");
                }
            }
            //If Cat is on wall
            else if (OnWall)
            {
                PlayerBrain.PB.rb.gravityScale = 0;
                PlayerBrain.PB.rb.AddForce(catDir, ForceMode2D.Impulse);//Applies force in towards the climbing wall

                //Checks if the player is pushing up or down
                if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>().y != 0)
                {
                    PlayerBrain.PB.rb.velocity += (Vector2.up * PlyCtrl.Player.FishInWater.ReadValue<Vector2>().y * speed * 0.5f) - new Vector2(0, PlayerBrain.PB.rb.velocity.y);
                    if (audioManager != null)
                    {
                        audioManager.Play("catClimb");
                    }
                }
                else if (audioManager != null)
                {
                    audioManager.Stop("catClimb");
                }
            }
            else if (!OnWall)
            {
                PlayerBrain.PB.rb.gravityScale = 1;
                if (audioManager != null)
                {
                    audioManager.Stop("catClimb");
                }
            }

            //Remove momentum while on wall
            if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>().y == 0 && OnWall)
            {
                PlayerBrain.PB.rb.velocity *= new Vector2(1, 0.5f);
            }

            #region Animation Block
            //Climbing on Wall as cat
            if (OnWall)
            {
                PlayerBrain.PB.plyAnim.SetBool("Climb", true);

                if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>().y != 0)
                {
                    PlayerBrain.PB.plyAnim.SetBool("Walking", true);
                }
                else
                {
                    PlayerBrain.PB.plyAnim.SetBool("Walking", false);
                }
            }
            else
            {
                PlayerBrain.PB.plyAnim.SetBool("Climb", false);
            }

            //Set direction as cat on wall
            if (OnWall && catDir == Vector2.right)
            {
                this.gameObject.transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y),
                    Mathf.Abs(transform.localScale.z));
                right = true;
                left = false;
            }
            else if (OnWall && catDir == Vector2.left)
            {
                this.gameObject.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y),
                    Mathf.Abs(transform.localScale.z));
                left = true;
                right = false;
            }
            #endregion
        }
    }

    public override void SetToDefault()
    {
        catDir = Vector2.zero;
        OnWall = false;
        PlayerBrain.PB.plyAnim.SetTrigger(form);
    }

    public void SetCatOnWall(bool value, Vector2 direction)
    {
        OnWall = value;
        catDir = direction;
    }

    public override void Jump()
    {
        if (PlayerBrain.PB.canJump)
        {
            if (isGrounded() && !OnWall)
            {
                PlayerBrain.PB.rb.AddForce((Vector2.up * jumpHeight) /*- new Vector2(0, rb.velocity.y)*/, ForceMode2D.Impulse);
            }
            //Side jump when climbing
            else if (OnWall)
            {
                PlayerBrain.PB.rb.AddForce((-catDir * 25) - new Vector2(PlayerBrain.PB.rb.velocity.x, 0), ForceMode2D.Impulse);
                catDir = -catDir;
            }

            base.Jump();
        }
    }

    public override void Special()
    {
        //Spawn hitbox
        Scratch();
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (PlayerBrain.PB.currentController == this)
        {
            if (other.CompareTag("Water"))
            {
                PlayerBrain.PB.plyAnim.SetTrigger("Death");
            }
        }
    }

    public override void ToggleBody(bool value)
    {
        Embodiment.canDisembody = value;
    }
}
