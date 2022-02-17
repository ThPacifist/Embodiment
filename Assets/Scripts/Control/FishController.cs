using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : Controller
{
    //Variables
    [Header("Fish Settings")]
    public Switch lever;
    public float waterDensity;
    float angle;

    //Protected variables
    protected bool inWater;

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //Movement when in water
        if (inWater)
        {
            //Move when the player is pressing buttons
            if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>() != Vector2.zero)
            {
                float x = 0, y = 0;
                //Checks if either the y or x velocity is exceeding the speed
                if (Mathf.Abs(PlayerBrain.PB.rb.velocity.x) < speed)
                {
                    x = 1;
                }
                if (Mathf.Abs(PlayerBrain.PB.rb.velocity.y) < speed)
                {
                    y = 1;
                }

                PlayerBrain.PB.rb.AddForce(new Vector2(x, y) * PlyCtrl.Player.FishInWater.ReadValue<Vector2>() * 20 * PlayerBrain.PB.rb.mass);
            }
        }
        //Movement when on the ground
        else if (isGrounded())
        {
            //Move when the player is pressing buttons
            if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
            {
                if (Mathf.Abs(PlayerBrain.PB.rb.velocity.x) < speed * 0.3f)
                {
                    PlayerBrain.PB.rb.AddForce(Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * 20 * PlayerBrain.PB.rb.mass);
                }
            }
        }
        else
        {
            //Move when the player is pressing the direction
            if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
            {
                PlayerBrain.PB.rb.velocity += (Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * speed * 0.5f) - new Vector2(PlayerBrain.PB.rb.velocity.x, 0);
            }
        }

        #region Animation Block
        //Moving in Water as fish
        if (inWater)
        {
            if (PlyCtrl.Player.FishInWater.ReadValue<Vector2>() != Vector2.zero)
            {
                PlayerBrain.PB.plyAnim.SetBool("Walking", true);
            }
            else
            {
                PlayerBrain.PB.plyAnim.SetBool("Walking", false);
            }
            angle = Vector2.SignedAngle(Vector2.left, PlyCtrl.Player.FishInWater.ReadValue<Vector2>());

            /*if(angle > 90)
            {
                angle = transform.localScale.x * (90 - (angle - 90));
            }
            else if(angle < -90)
            {
                angle = transform.localScale.x * (90 + (angle + 90));
            }*/

            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            //transform.rotation = rotation;
        }
        #endregion
    }

    //Set to default
    public override void SetToDefault()
    {
        inWater = false;
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        this.transform.rotation = rotation;
    }

    //Jump
    public override void Jump()
    {
        if (PlayerBrain.PB.canJump)
        {
            if (isGrounded() || PlayerBrain.PB.inWater)
            {
                PlayerBrain.PB.rb.AddForce((Vector2.up * jumpHeight) /*- new Vector2(0, rb.velocity.y)*/, ForceMode2D.Impulse);
            }

            base.Jump();// this goes last in function
        }
    }

    //Special
    public override void Special()
    {
        if (specialReady)
        {
            if (inWater)
                PlayerBrain.PB.plyAnim.SetTrigger("Spin");

            if (lever != null)
            {
                //Activate the lever
                lever.Interact();
                //Cooldown
                cooldownTime = 1;
                specialReady = false;
                StartCoroutine(SpecialCoolDown());
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (PlayerBrain.PB.currentController == this)
        {
            //If the collision is water, swim
            if (other.CompareTag("Water"))
            {
                if (audioManager != null)
                {
                    audioManager.Play("splash");
                }
                inWater = true;
                PlayerBrain.PB.plyCol.density = waterDensity;
            }
        }
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        if (PlayerBrain.PB.currentController == this)
        {
            if (other.CompareTag("Water"))
            {
                inWater = true;
                PlayerBrain.PB.plyAnim.SetBool("inWater", true);
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (PlayerBrain.PB.currentController == this)
        {
            //If the collision is water, stop swimming
            if (other.CompareTag("Water"))
            {
                inWater = false;
                PlayerBrain.PB.plyAnim.SetBool("inWater", false);
                PlayerBrain.PB.plyCol.density = density;
            }
        }
    }

    //Sets lever value
    public void SetLever(Switch l)
    {
        lever = l;
    }
}
