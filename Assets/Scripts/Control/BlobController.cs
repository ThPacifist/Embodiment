using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobController : Controller
{

    // Update is called once per frame
    public override void LateUpdate()
    {
        base.LateUpdate();

        if(canMove)
        {

        }

        if (!isGrounded())
        {
            if (audioManager != null)
            {
                audioManager.Stop("blobStep");
            }
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
        #endregion
    }

    public override void Jump()
    {
        if(canJump)
        {
            if (isGrounded() || inWater)
            {
                rb.AddForce((Vector2.up * jumpHeight) /*- new Vector2(0, rb.velocity.y)*/, ForceMode2D.Impulse);
            }
            else if (spcInter.isAttached)
            {
                spcInter.ShootTendril();
                rb.AddForce(rb.velocity.normalized * 5, ForceMode2D.Impulse);
            }

            base.Jump();// this goes last in function
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.CompareTag("Water"))
        {
            //Makes Blob float
            if (audioManager != null)
            {
                audioManager.Play("splash", true);
            }
            inWater = true;
            capCollider.density = 2;
            jumpHeight = 35;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);

        if (other.CompareTag("Water"))
        {
            //Blob jumps out of water
            inWater = false;
            capCollider.density = cntrlMove.defaultDensity;
            jumpHeight = cntrlMove.defaultJumpHeight;
        }
    }
}
