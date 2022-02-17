using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : Controller
{
    [Header("Human Settings")]
    public Rigidbody2D heldBox;
    [SerializeField]
    Transform heldPos;
    public bool boxHeld;
    public bool heavyBoxHeld;
    //[HideInInspector]
    public Rigidbody2D box;
    string boxTag;

    public override void FixedUpdate()
    {
        if (boxHeld)
        {
            heldBox.transform.position = heldPos.transform.position;
        }

        if (PlayerBrain.PB.canMove)
        {
            //Ground Movement
            if (Mathf.Abs(PlayerBrain.PB.rb.velocity.x) < speed)
            {
                PlayerBrain.PB.rb.AddForce(Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * 20 * PlayerBrain.PB.rb.mass);
            }
        }

        //Remove momentum while on ground
        if (PlyCtrl.Player.Movement.ReadValue<float>() == 0 && isGrounded())
        {
            //Reduce the player's speed by half
            PlayerBrain.PB.rb.velocity *= new Vector2(0.75f, 1);
            //Change any held boxes velocity to match the player
            if (heldBox != null)
            {
                heldBox.velocity = PlayerBrain.PB.rb.velocity;
            }
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

        //Pushing and Pulling boxes as Human
        if (!heavyBoxHeld)
        {
            if (PlayerBrain.PB.canMove)
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
        }
        else
        {
            //Set to Pushing or Pulling
            if (this.transform.localScale.x > 0)//When player is facing left, while holding a heavy box
            {
                if (PlyCtrl.Player.Movement.ReadValue<float>() > 0)//When player is pressing right
                {
                    PlayerBrain.PB.plyAnim.SetInteger("Facing", -1);
                }
                else if (PlyCtrl.Player.Movement.ReadValue<float>() < 0)//When player is pressing left
                {
                    PlayerBrain.PB.plyAnim.SetInteger("Facing", 1);
                }
                else
                {
                    PlayerBrain.PB.plyAnim.SetInteger("Facing", 0);
                }
            }
            else if (this.transform.localScale.x < 0)//When player is facing right, while holding a heavy box
            {
                if (PlyCtrl.Player.Movement.ReadValue<float>() > 0)//When player is pressing right
                {
                    PlayerBrain.PB.plyAnim.SetInteger("Facing", 1);
                }
                else if (PlyCtrl.Player.Movement.ReadValue<float>() < 0)//When player is pressing left
                {
                    PlayerBrain.PB.plyAnim.SetInteger("Facing", -1);
                }
                else
                {
                    PlayerBrain.PB.plyAnim.SetInteger("Facing", 0);
                }
            }
        }
        #endregion
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (PlayerBrain.PB.currentController == this)
        {
            if (other.CompareTag("Water"))
            {
                Debug.Log("Inside Water check: Human");
                PlayerBrain.PB.plyAnim.SetTrigger("Death");
            }
        }
    }

    public override void Special()
    {
        //Check if there is a box to hold or a box being held
        if (box != null && !boxHeld)
        {
            if (boxTag == "LBox" || boxTag == "MBox")
            {
                if (!CheckSpaceForBox(box))
                {
                    Debug.Log("Inside check space for box");
                    if (!Left && !Right)
                    {
                        PlayerBrain.PB.plyAnim.SetBool("isGrabbing", true);
                    }
                    else
                    {
                        PickUpBoxHuman(box);
                    }
                }
                else
                {
                    Debug.LogError("Not Enough space for box");
                }
            }
            else if (boxTag == "HBox")
            {
                if (audioManager != null)
                {
                    audioManager.Play("boxGrab");
                }
                PlayerBrain.PB.plyAnim.SetBool("isPushing", true);

                //Attach Box
                heavyBoxHeld = true;
                PlayerBrain.PB.fixedJ.enabled = true;
                PlayerBrain.PB.fixedJ.connectedBody = box;
                PlayerBrain.PB.fixedJ.connectedBody.mass = 6;
                speed = 3;
            }
        }
        else if (boxHeld || heavyBoxHeld)
        {
            if (!Left && !Right)
            {
                PlayerBrain.PB.plyAnim.SetBool("isGrabbing", false);
                PickUpBoxHuman(null);
            }
            else
            {
                PickUpBoxHuman(null);
            }

            if (heavyBoxHeld)
            {
                PickUpBoxHuman(null);
            }
        }
    }

    public override void Jump()
    {
        if (PlayerBrain.PB.canJump)
        {
            if (isGrounded() || PlayerBrain.PB.inWater)
            {
                PlayerBrain.PB.rb.AddForce((Vector2.up * jumpHeight) /*- new Vector2(0, rb.velocity.y)*/, ForceMode2D.Impulse);
            }

            base.Jump();
        }
    }

    public override void SetToDefault()
    {
        PlayerBrain.PB.plyAnim.SetBool("isGrabbing", false);
        boxHeld = false;
        heavyBoxHeld = false;
        boxTag = null;
        PlayerBrain.PB.fixedJ.enabled = false;
        if (boxTag == "HBox")
            PlayerBrain.PB.fixedJ.connectedBody.mass = 20;
        speed = 5;
        PlayerBrain.PB.fixedJ.connectedBody = null;
        heldBox = null;
        box = null;
    }

    //Sets the value of the held box
    public void SetHeldBox(Rigidbody2D rb, string inputTag)
    {
        box = rb;
        boxTag = inputTag;
        if (PlayerBrain.PB.prefabInstance == null)
        {
            PlayerBrain.PB.prefabInstance = Instantiate(PlayerBrain.PB.IndicatorPrefab, box.transform);
        }
        else if (rb == null)
        {
            Destroy(PlayerBrain.PB.prefabInstance);
        }
        else if (PlayerBrain.PB.prefabInstance != null)
        {
            Destroy(PlayerBrain.PB.prefabInstance);
            PlayerBrain.PB.prefabInstance = Instantiate(PlayerBrain.PB.IndicatorPrefab, box.transform);
        }
    }

    public void PickUpBoxHuman(Rigidbody2D box)
    {
        if (box != null)
        {
            if (audioManager != null)
            {
                audioManager.Play("boxGrab");
            }
            PlayerBrain.PB.plyAnim.SetBool("isGrabbing", true);
            //Attach box
            heldBox = box;
            boxHeld = true;
            PlayerBrain.PB.fixedJ.enabled = true;
            PlayerBrain.PB.fixedJ.connectedBody = heldBox;
            heldBox.transform.position = heldPos.transform.position;
        }
        else
        {
            if (audioManager != null)
            {
                audioManager.Play("boxGrab");
            }
            PlayerBrain.PB.plyAnim.SetBool("isPushing", false);
            PlayerBrain.PB.plyAnim.SetBool("isGrabbing", false);
            boxHeld = false;
            heavyBoxHeld = false;
            PlayerBrain.PB.fixedJ.enabled = false;
            if (boxTag == "HBox")
                PlayerBrain.PB.fixedJ.connectedBody.mass = 20;
            speed = 5;
            PlayerBrain.PB.fixedJ.connectedBody = null;
            heldBox = null;
        }
    }

    //Checks to see if there is enough space for the box, so that it does not get put through a wall
    bool CheckSpaceForBox(Rigidbody2D rb)
    {
        BoxCollider2D[] cols = new BoxCollider2D[2];
        rb.GetAttachedColliders(cols); //Gets all the colliders attached to a box
        BoxCollider2D col;

        //Determines which collider is the box collider, versus the trigger collider
        if (cols[0].isTrigger)
        {
            col = cols[1];
        }
        else
        {
            col = cols[0];
        }

        float dist = 0;
        int layer = LayerMask.NameToLayer("CheckSpace");
        //Casts a box (much like raycast) into the scene
        RaycastHit2D hit = Physics2D.BoxCast(heldPos.position, col.size, 0f, Vector2.down, dist, layer);

        return hit.collider != null; //If collider exists, sends true. Otherwise false
    }

    public override void CallFromAnimation(int value)
    {
        if(PlayerBrain.PB.currentController == this)
        {
            if (value == 0)
            {
                PickUpBoxHuman(box);
            }
            else
            {
                PickUpBoxHuman(null);
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Human Box Pos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(heldPos.position, 0.3f);
    }
}
