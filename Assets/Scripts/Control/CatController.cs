using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : Controller
{
    [Header("Cat Settings")]
    public bool OnWall;
    Vector2 catDir;
    [HideInInspector]
    public bool treadmill = false;

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //Regular grounded movement
        if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
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
        else if(!OnWall)
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
}
