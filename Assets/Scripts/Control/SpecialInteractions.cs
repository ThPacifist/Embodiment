using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;
using System.Security.Cryptography;
using UnityEngine.UIElements;

public class SpecialInteractions : MonoBehaviour
{
    //Fish Tail spin Interaction done by Benathen 9/7
    //Initial code started by Jason on 9/1
    //Picking up and dropping boxes done on by Jason 9/2
    /*
     * TODO:
     * 
     */

        //IMPORTANT: For any successfully executed special action set a cooldown time, unset special ready, and call the cooldown timer!!!

    //Public variables and assets
    public Transform player;
    public PlyController plyCntrl;
    public Rigidbody2D plyRb;
    public Rigidbody2D heldBox;
    public SkeletonTrigger heldSkel;
    public Collider2D plyCol;
    public GameObject attackBox;
    public GameObject lamp;
    public SpringJoint2D spring;
    public static Action Climb = delegate { };
    public static Action Scratch = delegate { };
    public LineRenderer lineRender;
    public bool isAttached;
    public bool objectHeld;
    public bool skelHeld;
    public bool HboxHeld;

    //Private variables
    private bool specialReady = true;
    private float cooldownTime;
    Rigidbody2D box;
    SkeletonTrigger skeleton;
    
    Switch lever;
    string boxTag;
    AudioManager audioManager;

    [SerializeField]
    Transform HheldPos;
    [SerializeField]
    Transform BheldPos;
    [SerializeField]
    Transform skelHeldPos;

    [SerializeField]
    FixedJoint2D fixedJ;

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        PlyController.Special += WaterSpecial;
        PlyController.Special += LandSpecial;
        PlyController.Special += AirSpecial;
    }

    private void OnDisable()
    {
        PlyController.Special -= WaterSpecial;
        PlyController.Special -= LandSpecial;
        PlyController.Special -= AirSpecial;
    }

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();

        spring.enabled = false;
        fixedJ.enabled = false;

        lineRender.positionCount = 2;
        lineRender.SetPosition(0, player.position);//Starting Position of Tendril Line
        lineRender.SetPosition(1, player.position);//Ending Position of Tendril Line
        lineRender.enabled = false;
    }

    private void FixedUpdate()
    {
        lineRender.SetPosition(0, player.position);
        if (!isAttached)
        {
            lineRender.SetPosition(1, player.position);
        }
        else
        {
            //Gets the vector that starts from the lamp position and goes to the player position
            Vector2 targetDir = player.position - lamp.transform.position;
            //Gets the angle of the player again, except returns negative angle when the player is to the right of the swing
            float signedAngle = Vector2.SignedAngle(targetDir, Vector2.down);
            //Changes the player's rotation to be relative to the swing
            Quaternion rotation = Quaternion.Euler(0, 0, -signedAngle);
            this.transform.rotation = rotation;
        }

        if(plyCntrl.Left)
        {
            attackBox.transform.position = new Vector2(player.transform.position.x + -1f, attackBox.transform.position.y);
        }
        else if(plyCntrl.Right)
        {
            attackBox.transform.position = new Vector2(player.transform.position.x + 1f, attackBox.transform.position.y);
        }

        if(objectHeld)
        {
            if (player.tag == "Human")
            {
                heldBox.transform.position = HheldPos.transform.position;
            }
            else if (player.tag == "Bat")
            {
                //heldBox.transform.position = BheldPos.transform.position;
            }
        }
        if(skelHeld)
        {
            if(player.tag == "Blob")
            {
                heldSkel.skelGObject.transform.position = skelHeldPos.transform.position;
            }
        }
    }

    //Special for fish is going to be written by Benathen
    private void WaterSpecial()
    {
        if (specialReady)
        {
            if (lever != null)
            {
                //Activate the lever
                lever.Interact();
                //Cooldown
                cooldownTime = 2;
                specialReady = false;
                StartCoroutine(SpecialCoolDown());
            }
        }
    }

    //Special for blob is tendril grab and swing
    //Special for cat is climb or cat scratch
    //Special for human is to pick up boxes up to medium weight
    private void LandSpecial()
    {
        if (specialReady)
        {
            switch (player.tag)
            {
                case "Blob":
                    //Pick up Skeleton
                    if(skeleton != null && !isAttached && !skelHeld)
                    {
                        PickUpSkeleton(skeleton);
                    }
                    //Tentacle swing
                    else if (lamp != null && !skelHeld)
                    {
                        ShootTentril();
                    }
                    //Drop skeleton
                    else if(skelHeld)
                    {
                        PickUpSkeleton(null);
                    }

                    break;
                case "Human":
                    //Check if there is a box to hold or a box being held
                    if (box != null && !objectHeld)
                    {
                        if (boxTag == "LBox" || boxTag == "MBox")
                        {
                            if (audioManager != null)
                            {
                                audioManager.Play("boxGrab", true);
                            }
                            //Attach box
                            //box.transform.parent = player;
                            heldBox = box;
                            //heldBox.gravityScale = 0;
                            //heldBox.freezeRotation = true;
                            objectHeld = true;
                            fixedJ.enabled = true;
                            fixedJ.connectedBody = heldBox;
                            heldBox.transform.position = HheldPos.transform.position;
                            //Cooldown
                            cooldownTime = 1;
                            specialReady = false;
                            StartCoroutine("SpecialCoolDown");
                        }
                        else if(boxTag == "HBox")
                        {
                            if (audioManager != null)
                            {
                                audioManager.Play("boxGrab", true);
                            }
                            //Attach Box
                            HboxHeld = true;
                            fixedJ.enabled = true;
                            fixedJ.connectedBody = box;

                            //Cooldown
                            cooldownTime = 1;
                            specialReady = false;
                            StartCoroutine("SpecialCoolDown");
                        }
                    }
                    else if (objectHeld || HboxHeld)
                    {
                        if (audioManager != null)
                        {
                            audioManager.Play("boxGrab", true);
                        }
                        //Drop box
                        objectHeld = false;
                        HboxHeld = false;
                        //heldBox.transform.parent = null;
                        //heldBox.gravityScale = 1;
                        //heldBox.freezeRotation = false;
                        fixedJ.enabled = false;
                        fixedJ.connectedBody = null;
                        heldBox = null;
                        //Cooldown
                        cooldownTime = 1;
                        specialReady = false;
                        StartCoroutine("SpecialCoolDown");
                    }
                    break;
                case "Cat":
                    //Spawn hitbox
                    Scratch();
                    //Cooldown
                    cooldownTime = 1;
                    specialReady = false;
                    StartCoroutine("SpecialCoolDown");
                    break;
            }
        }
    }

    //Special for bat is to pick up very light boxes
    private void AirSpecial()
    {
        if (specialReady)
        {
            //Attach box
            if (box != null && !objectHeld)
            {
                if (boxTag == "LBox")
                {
                    if (audioManager != null)
                    {
                        audioManager.Play("boxGrab", true);
                    }
                    //box.transform.parent = player;
                    heldBox = box;
                    //heldBox.gravityScale = 0;
                    objectHeld = true;
                    fixedJ.enabled = true;
                    fixedJ.connectedBody = heldBox;
                    heldBox.transform.position = BheldPos.transform.position;
                    //Cooldown
                    cooldownTime = 1;
                    specialReady = false;
                    StartCoroutine("SpecialCoolDown");
                }
            }
            else if (objectHeld)
            {
                if (audioManager != null)
                {
                    audioManager.Play("boxGrab", true);
                }
                //Drop box
                Debug.Log("Drop box");
                //heldBox.transform.parent = null;
                //heldBox.gravityScale = 1;
                objectHeld = false;
                fixedJ.enabled = false;
                fixedJ.connectedBody = null;
                heldBox = null;
                //Cooldown
                cooldownTime = 1;
                specialReady = false;
                StartCoroutine("SpecialCoolDown");
            }
        }
    }

    //Check for boxes
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if it is a switch
        if(other.CompareTag("Lever"))
        {
            lever = other.GetComponent<Switch>();
        }
    }
    //Sets the value of the lamp
    public void SetSwingerGameObject(GameObject value) { lamp = value; }

    //Sets the value of the held box
    public void SetHeldBox(Rigidbody2D rb, string inputTag) 
    { 
        box = rb;
        boxTag = inputTag;
    }

    public void SetHeldSkel(SkeletonTrigger skel)
    {
        skeleton = skel;
    }
    //Remove boxes from selection
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Lever"))
        {
            lever = null;
        }
    }

    //Special cooldown
    IEnumerator SpecialCoolDown()
    {
        yield return new WaitForSeconds(cooldownTime);
        specialReady = true;
    }


    //Creates the tentacle between the lamp and the player
    public void ShootTentril()
    {
        //Debug.Log("Spring on " + spring.isActiveAndEnabled);
        if (audioManager != null)
        {
            audioManager.Play("Swing", true);
        }
        if (!spring.isActiveAndEnabled)
        {
            plyCntrl.move = false;
            lineRender.enabled = true;
            spring.enabled = true;
            spring.connectedAnchor = lamp.transform.position;
            lineRender.SetPosition(1, new Vector3(lamp.transform.position.x, lamp.transform.position.y, player.position.z));
            isAttached = true;

            //Cooldown
            cooldownTime = 0.3f;
            specialReady = false;
        }
        else
        {
            plyCntrl.move = true;
            spring.enabled = false;
            spring.connectedAnchor = Vector2.zero;
            lineRender.SetPosition(1, player.position);
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

    public void PickUpSkeleton(SkeletonTrigger skelo)
    {
        if (skelo != null && !skelHeld)
        {
            heldSkel = skelo;
            heldSkel.isGrabbed = true;
            heldSkel.indicator.SetActive(false);
            skelHeld = true;
            //fixedJ.enabled = true;
            fixedJ.connectedBody = heldSkel.rigidbody;
            heldSkel.skelGObject.transform.position = skelHeldPos.transform.position;
        }
        else if(skelo == null && skelHeld)
        {
            heldSkel.isGrabbed = false;
            heldSkel = null;
            skelHeld = false;
            fixedJ.enabled = false;
            fixedJ.connectedBody = null;
        }

        //Cooldown
        cooldownTime = 1;
        specialReady = false;
        StartCoroutine("SpecialCoolDown");
    }
}
