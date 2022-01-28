using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobController : Controller
{
    public bool isAttached;
    public bool skelHeld;
    public LineRenderer lRenderer;
    public GameObject lamp;
    public SpringJoint2D spring;
    public SkeletonTrigger heldSkel;
    [SerializeField]
    GameObject IndicatorPrefab;
    public GameObject prefabInstance;

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        lRenderer.SetPosition(0, transform.position);
        if (!isAttached)
        {
            lRenderer.SetPosition(1, transform.position);
        }
        else
        {
            //Gets the vector that starts from the lamp position and goes to the player position
            Vector2 targetDir = transform.position - lamp.transform.position;
            //Gets the angle of the player again, except returns negative angle when the player is to the right of the swing
            float signedAngle = Vector2.SignedAngle(targetDir, Vector2.down);
            //Changes the player's rotation to be relative to the swing
            Quaternion rotation = Quaternion.Euler(0, 0, -signedAngle);
            this.transform.rotation = rotation;
        }

        if (canMove)
        {
            if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
            {
                //Movement
                if (Mathf.Abs(rb.velocity.x) < speed && !isAttached)
                {
                    rb.AddForce(Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * 20 * rb.mass);
                }
                else if (isAttached)
                {
                    if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                    {
                        rb.AddForce(Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * 0.6f, ForceMode2D.Impulse);
                    }
                }
            }
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

    public override void Special()
    {
        //Pick up Skeleton
        if (skeleton != null && !isAttached && !skelHeld)
        {
            if (!CheckSpaceForSkelo())
            {
                if (!plyCntrl.Left && !plyCntrl.Right)
                {
                    plyAnim.SetBool("isGrabbing", true);
                }
                else
                {
                    PickUpSkeleton(skeleton);
                }
            }
            else
            {
                Debug.LogError("There is not enough space for skeleton");
            }
        }
        //Tentacle swing
        else if (lamp != null && !skelHeld)
        {
            ShootTendril();
        }
        //Drop skeleton
        else if (skelHeld)
        {
            if (!plyCntrl.Left && !plyCntrl.Right)
            {
                plyAnim.SetBool("isGrabbing", false);
            }
            else
            {
                PickUpSkeleton(null);
            }
        }
    }

    //Checks to see if there is enough space for the skeleton, so that it does not get put through a wall
    bool CheckSpaceForSkelo()
    {
        Vector2 start = skelHeldPos.position + Vector3.up; //Pos of skelHeldPos up one
        float dist = 0.2f;

        string name = "Jumpables";
        int layerMask = LayerMask.NameToLayer(name);

        RaycastHit2D hit = Physics2D.Raycast(start, Vector2.down, dist, layerMask);//Sends a ray from start and will only hit colliders in Jumpables layer
        Debug.DrawRay(start, Vector2.down, Color.red);

        return hit.collider != null; //If collider exists, sends true. Otherwise false
    }

    //Creates the tentacle between the lamp and the player
    public void ShootTendril()
    {
        //Debug.Log("Spring on " + spring.isActiveAndEnabled);
        if (audioManager != null)
        {
            audioManager.Play("Swing");
        }
        if (!spring.isActiveAndEnabled)
        {
            lineRender.enabled = true;
            spring.enabled = true;
            spring.connectedAnchor = lamp.transform.position;
            lineRender.SetPosition(1, new Vector3(lamp.transform.position.x, lamp.transform.position.y, transform.position.z));
            isAttached = true;

            //Cooldown
            cooldownTime = 0.3f;
            specialReady = false;
        }
        else
        {
            plyCntrl.canMove = true;
            spring.enabled = false;
            spring.connectedAnchor = Vector2.zero;
            lineRender.SetPosition(1, transform.position);
            lineRender.enabled = false;
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            this.transform.rotation = rotation;
            isAttached = false;

            //Cooldown
            cooldownTime = 0.5f;
            specialReady = false;
        }
        //init Cooldown
        StartCoroutine("SpecialCoolDown");
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.CompareTag("Water"))
        {
            //Makes Blob float
            if (audioManager != null)
            {
                audioManager.Play("splash");
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
