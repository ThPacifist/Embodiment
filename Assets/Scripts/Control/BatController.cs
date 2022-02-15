using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : Controller
{
    [Header("Bat Settings")]
    bool boxHeld;
    public Rigidbody2D heldBox;
    [SerializeField]
    Transform heldPos;
    [HideInInspector]
    public Rigidbody2D box;
    string boxTag;

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(PlayerBrain.PB.canMove)
        {
            //Ground Movement
            if (Mathf.Abs(PlayerBrain.PB.rb.velocity.x) < speed)
            {
                PlayerBrain.PB.rb.AddForce(Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * 20 * PlayerBrain.PB.rb.mass);
            }
        }
    }

    public override void Jump()
    {

        base.Jump();
    }

    public override void Special()
    {
        //Attach box
        if (box != null && !boxHeld)
        {
            if (boxTag == "LBox")
            {
                if (!Left && !Right)
                {
                    PlayerBrain.PB.plyAnim.SetBool("isGrabbing", true);
                }
                else
                {
                    PickUpBoxBat(box);
                }
            }
        }
        else if (boxHeld)
        {
            PlayerBrain.PB.plyAnim.SetBool("isGrabbing", false);
            PickUpBoxBat(null);
        }
    }

    public void PickUpBoxBat(Rigidbody2D box)
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
            PlayerBrain.PB.plyAnim.SetBool("isGrabbing", false);
            //plyAnim.SetTrigger("Bat");
            boxHeld = false;
            PlayerBrain.PB.fixedJ.enabled = false;
            PlayerBrain.PB.fixedJ.connectedBody = null;
            heldBox = null;
        }

        //Cooldown
        cooldownTime = 1;
        specialReady = false;
        StartCoroutine("SpecialCoolDown");
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

    public override void CallFromAnimation(int value)
    {
        if(value == 0)
        {
            PickUpBoxBat(box);
        }
        else
        {
            PickUpBoxBat(null);
        }
    }
}
