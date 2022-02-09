using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobController : Controller
{
    [Header("Blob Settings")]
    public bool isAttached;
    public bool skelHeld;
    public LineRenderer lRenderer;
    public GameObject lamp;
    public SkeletonTrigger heldSkel;
    [HideInInspector]
    public SkeletonTrigger skeleton;
    [SerializeField]
    Transform skelHeldPos;

    public override void Start()
    {
        base.Start();

        lRenderer.positionCount = 2;
        lRenderer.SetPosition(0, transform.position);//Starting Position of Tendril Line
        lRenderer.SetPosition(1, transform.position);//Ending Position of Tendril Line
        lRenderer.enabled = false;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (PlayerBrain.PB.canMove)
        {
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
            if (skelHeld)
            {
                heldSkel.skelGObject.transform.position = skelHeldPos.transform.position;
            }

            if (PlayerBrain.PB.canMove)
            {
                if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                {
                    //Movement
                    if (Mathf.Abs(PlayerBrain.PB.rb.velocity.x) < speed && !isAttached)
                    {
                        PlayerBrain.PB.rb.AddForce(Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * 20 * PlayerBrain.PB.rb.mass);
                    }
                    else if (isAttached)
                    {
                        if (PlyCtrl.Player.Movement.ReadValue<float>() != 0)
                        {
                            PlayerBrain.PB.rb.AddForce(Vector2.right * PlyCtrl.Player.Movement.ReadValue<float>() * 0.6f, ForceMode2D.Impulse);
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
            //Check if the blob is attached
            if (isAttached)
            {
                PlayerBrain.PB.plyAnim.SetBool("Swing", true);
            }
            else
            {
                PlayerBrain.PB.plyAnim.SetBool("Swing", false);
            }
            #endregion
        }
    }

    public override void Jump()
    {
        if(PlayerBrain.PB.canJump)
        {
            if (isGrounded() || PlayerBrain.PB.inWater)
            {
                PlayerBrain.PB.rb.AddForce((Vector2.up * jumpHeight) /*- new Vector2(0, rb.velocity.y)*/, ForceMode2D.Impulse);
            }
            else if (isAttached)
            {
                ShootTendril();
                PlayerBrain.PB.rb.AddForce(PlayerBrain.PB.rb.velocity.normalized * 5, ForceMode2D.Impulse);
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
                if (!Left && !Right)
                {
                    PlayerBrain.PB.plyAnim.SetBool("isGrabbing", true);
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
            if (!Left && !Right)
            {
                PlayerBrain.PB.plyAnim.SetBool("isGrabbing", false);
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

        string name = "CheckSpace";
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
        if (!PlayerBrain.PB.spring.isActiveAndEnabled)
        {
            lRenderer.enabled = true;
            PlayerBrain.PB.spring.enabled = true;
            PlayerBrain.PB.spring.connectedAnchor = lamp.transform.position;
            lRenderer.SetPosition(1, new Vector3(lamp.transform.position.x, lamp.transform.position.y, transform.position.z));
            isAttached = true;

            //Cooldown
            cooldownTime = 0.3f;
            specialReady = false;
        }
        else
        {
            PlayerBrain.PB.spring.enabled = false;
            PlayerBrain.PB.spring.connectedAnchor = Vector2.zero;
            lRenderer.SetPosition(1, transform.position);
            lRenderer.enabled = false;
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

    //Sets the value of the lamp variable to allow the player to swing
    public void SetSwingerGameObject(GameObject value)
    {
        lamp = value;
        if (PlayerBrain.PB.prefabInstance == null)
        {
            PlayerBrain.PB.prefabInstance = Instantiate(PlayerBrain.PB.IndicatorPrefab, lamp.transform);
        }
        else if (value == null)
        {
            Destroy(PlayerBrain.PB.prefabInstance);
        }
        else if (PlayerBrain.PB.prefabInstance != null)
        {
            Destroy(PlayerBrain.PB.prefabInstance);
            PlayerBrain.PB.prefabInstance = Instantiate(PlayerBrain.PB.IndicatorPrefab, lamp.transform);
        }
    }

    //This function is called when we want the blob to pick up skeleton
    public void PickUpSkeleton(SkeletonTrigger skelo)
    {
        if (skelo != null && !skelHeld)
        {
            heldSkel = skelo;
            heldSkel.isGrabbed = true;
            Destroy(PlayerBrain.PB.prefabInstance);
            skelHeld = true;
            PlayerBrain.PB.fixedJ.enabled = true;
            PlayerBrain.PB.fixedJ.connectedBody = heldSkel.transform.parent.GetComponent<Rigidbody2D>();
            heldSkel.skelGObject.transform.position = skelHeldPos.transform.position;
            PlayerBrain.PB.plyAnim.SetBool("isGrabbing", true);
            jumpHeight = 60;

            Debug.Log("Picked up " + skelo.name);
        }
        else if (skelo == null && skelHeld)
        {
            heldSkel.isGrabbed = false;
            heldSkel = null;
            skelHeld = false;
            PlayerBrain.PB.fixedJ.enabled = false;
            PlayerBrain.PB.fixedJ.connectedBody = null;
            PlayerBrain.PB.plyAnim.SetBool("isGrabbing", false);
            jumpHeight = 18.1f;

            Debug.Log("Put Down my object");
        }

        //Cooldown
        cooldownTime = 1;
        specialReady = false;
        StartCoroutine("SpecialCoolDown");
    }

    //Sets the value of the skeleton to be held
    public void SetHeldSkel(SkeletonTrigger skel)
    {
        skeleton = skel;
        if (PlayerBrain.PB.prefabInstance == null)
        {
            PlayerBrain.PB.prefabInstance = Instantiate(PlayerBrain.PB.IndicatorPrefab, skeleton.transform);
        }
        else if (skel == null)
        {
            Destroy(PlayerBrain.PB.prefabInstance);
        }
        else if (PlayerBrain.PB.prefabInstance != null)
        {
            Destroy(PlayerBrain.PB.prefabInstance);
            PlayerBrain.PB.prefabInstance = Instantiate(PlayerBrain.PB.IndicatorPrefab, skeleton.transform);
        }
    }

    public override void CallFromAnimation(int value)
    {
        if(value == 0)
        {
            PickUpSkeleton(skeleton);
        }
        else
        {
            PickUpSkeleton(null);
        }
    }


    //OnTriggers
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
            PlayerBrain.PB.inWater = true;
            PlayerBrain.PB.plyCol.density = 2;
            jumpHeight = 35;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);

        if (other.CompareTag("Water"))
        {
            //Blob jumps out of water
            PlayerBrain.PB.inWater = false;
            PlayerBrain.PB.plyCol.density = PlayerBrain.PB.CM.defaultDensity;
            jumpHeight = PlayerBrain.PB.CM.defaultJumpHeight;
        }
    }

    private void OnDrawGizmos()
    {
        //Blob Box Pos
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(skelHeldPos.position, 0.3f);
    }
}
