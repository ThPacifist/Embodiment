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
    public Collider2D plyCol;
    public GameObject attackBox;
    public GameObject lamp;
    public GameObject tendril;
    public SpringJoint2D spring;
    public static Action Climb = delegate { };
    public static Action<Transform> SelectBox = delegate { };
    public LineRenderer lineRender;
    public bool isAttached;
    public float angle;
    public float maxAngle;
    public bool objectHeld;

    //Private variables
    private bool climb;
    private bool canHold;
    private bool specialReady = true;
    private float timeElapsed;
    private float cooldownTime;
    Rigidbody2D box;
    public Rigidbody2D heldBox;
    Switch lever;
    string boxTag;

    [SerializeField]
    Transform HheldPos;
    [SerializeField]
    Transform BheldPos;   
    GameObject indicatorPrefabClone;

    [SerializeField]
    FixedJoint2D fixedJ;

    public bool ObjectHeld
    { get { return objectHeld; } }

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
            plyCntrl.move = true;
            lineRender.SetPosition(1, player.position);
        }
        else
        {
            plyCntrl.move = false;
            //Gets the vector that starts from the lamp position and goes to the player position
            Vector2 targetDir = player.position - lamp.transform.position;
            //Gets the angle of the player again, except returns negative angle when the player is to the right of the swing
            float signedAngle = Vector2.SignedAngle(targetDir, Vector2.down);
            //Changes the player's rotation to be relative to the swing
            Quaternion rotation = Quaternion.Euler(0, 0, -signedAngle);
            this.transform.rotation = rotation;
        }

        if(objectHeld)
        {
            if (player.tag == "Human")
            {
                heldBox.transform.position = HheldPos.transform.position;
            }
            else if(player.tag == "Bat")
            {
                heldBox.transform.position = BheldPos.transform.position;
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
                    //Tendril swing
                    if(lamp != null)
                    {
                        ShootTentril();
                    }

                    break;
                case "Human":
                    //Check if there is a box to hold or a box being held
                    if (box != null && !objectHeld)
                    {
                        if (boxTag == "LBox" || boxTag == "MBox")
                        {
                            //Attach box
                            //box.transform.parent = player;
                            heldBox = box;
                            heldBox.sharedMaterial.friction = 0;
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
                    }
                    else if (objectHeld)
                    {
                        //Drop box
                        objectHeld = false;
                        heldBox.sharedMaterial.friction = 0.6f;
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
                    attackBox.SetActive(true);
                    StartCoroutine(DeleteBox());
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
        //Check if it is a cat climb wall
        else if(other.CompareTag("Climb"))
        {
            climb = true;
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
    //Remove boxes from selection
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Lever"))
        {
            lever = null;
        }
        else if(other.CompareTag("Climb"))
        {
            if (climb)
            {
                Climb();
                climb = false;
            }
        }
        else if(other.CompareTag("Swing"))
        {
            if (!isAttached)
            {
                lamp = null;
            }
        }

        DestroyIndicator();
    }

    //Special cooldown
    IEnumerator SpecialCoolDown()
    {
        yield return new WaitForSeconds(cooldownTime);
        specialReady = true;
    }

    //Disable hitbox
    IEnumerator DeleteBox()
    {
        yield return new WaitForSeconds(cooldownTime);
        attackBox.SetActive(false);
    }

    //Creates the tentacle between the lamp and the player
    public void ShootTentril()
    {
        Debug.Log("Spring on" + spring.isActiveAndEnabled);
        if (!spring.isActiveAndEnabled)
        {
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

    //Creation and Destruction the indicator animation prefav
   
    void DestroyIndicator()
    {
        Destroy(indicatorPrefabClone);
        indicatorPrefabClone = null;
    }
}
