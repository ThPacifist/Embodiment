using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : Controller
{
    //Variables
    [Header("Fish Settings")]
    public Switch lever;
    public float waterDensity;

    //Protected variables
    protected bool inWater;

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //Handles movemnt when on the land
        if (PlyCtrl.Player.Movement.ReadValue<float>() != 0 && !inWater)
        {
            //Movement
            if (Mathf.Abs(PlayerBrain.PB.rb.velocity.x) < speed )
            {
                PlayerBrain.PB.rb.AddForce(Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * 20 * PlayerBrain.PB.rb.mass);
            }
        }
        //Handles vertical movement when in water
        else if(PlyCtrl.Player.FishInWater.ReadValue<Vector2>() != Vector2.zero)
        {
            if (Mathf.Abs(PlayerBrain.PB.rb.velocity.x + PlayerBrain.PB.rb.velocity.y) < speed)
            {
                PlayerBrain.PB.rb.AddForce(new Vector2(1, 1) * PlyCtrl.Player.FishInWater.ReadValue<Vector2>() * 20 * PlayerBrain.PB.rb.mass);
            }
        }

        #region Animation Block
        //Check if the blob is attached
        if (inWater)
        {
            PlayerBrain.PB.plyAnim.SetBool("inWater", true);
        }
        else
        {
            PlayerBrain.PB.plyAnim.SetBool("inWater", false);
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
        //If the Trigger is Death, trigger Death
        if (other.CompareTag("Death") || other.CompareTag("Skeleton"))
        {
            PlayerBrain.PB.plyAnim.SetTrigger("Death");
        }

        //If the collision is water, swim
        if(other.CompareTag("Water"))
        {
            inWater = true;
            PlayerBrain.PB.plyCol.density = waterDensity;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        //If the collision is water, stop swimming
        if(other.CompareTag("Water"))
        {
            inWater = false;
            PlayerBrain.PB.plyCol.density = density;
        }
    }

    //Sets lever value
    public void SetLever(Switch l)
    {
        lever = l;
    }
}
