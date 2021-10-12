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

    //Private variables
    private bool climb;
    private bool canHold;
    private bool objectHeld;
    private bool specialReady = true;
    private float timeElapsed;
    private float cooldownTime;
    Rigidbody2D box;
    private Rigidbody2D heldBox;
    private Vector3 direction;
    Switch lever;
    bool inRange;

    [SerializeField]
    Transform HheldPos;
    [SerializeField]
    Transform BheldPos;
    [SerializeField]
    GameObject indicatorPrefab;
    GameObject indicatorPrefabClone;

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
        lineRender.positionCount = 2;
        lineRender.SetPosition(0, player.position);//Starting Position of Tendril Line
        lineRender.SetPosition(1, player.position);//Ending Position of Tendril Line
        lineRender.startColor = Color.black;
        lineRender.startColor = Color.gray;
        lineRender.startWidth = 0.5f;
        lineRender.endWidth = 0.1f;
    }

    private void Update()
    {
        lineRender.SetPosition(0, player.position);
        if (!isAttached)
        {
            lineRender.SetPosition(1, player.position);
        }

        if(objectHeld)
        {
            if (player.tag == "Human")
            {
                heldBox.transform.position = HheldPos.transform.position;
                if (PlyController.Right)
                {
                    HheldPos.transform.localPosition = new Vector2(Mathf.Abs(HheldPos.transform.localPosition.x), HheldPos.transform.localPosition.y);
                }
                else if (PlyController.Left)
                {
                    HheldPos.transform.localPosition = new Vector2(Mathf.Abs(HheldPos.transform.localPosition.x) * -1, HheldPos.transform.localPosition.y);
                }
            }
            else if(player.tag == "Bat")
            {
                heldBox.transform.position = BheldPos.transform.position;
                /*if (PlyController.Right)
                {
                    BheldPos.transform.localPosition = new Vector2(Mathf.Abs(BheldPos.transform.localPosition.x), BheldPos.transform.localPosition.y);
                }
                else if (PlyController.Left)
                {
                    BheldPos.transform.localPosition = new Vector2(Mathf.Abs(BheldPos.transform.localPosition.x) * -1, BheldPos.transform.localPosition.y);
                }*/
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
                    if(inRange)
                    {
                        ShootTentril();
                    }

                    break;
                case "Human":
                    //Check if there is a box to hold or a box being held
                    if (canHold && !objectHeld)
                    {
                        if (box != null)
                        {
                            //Attach box
                            box.transform.parent = player;
                            //box.position = heldPos.position;
                            heldBox = box;
                            heldBox.gravityScale = 0;
                            objectHeld = true;
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
                        heldBox.transform.parent = null;
                        heldBox.gravityScale = 1;
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
                    StartCoroutine("DeleteBox");
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
            if (canHold && !objectHeld)
            {
                //Attach box
                if (box != null)
                {
                    box.transform.parent = player;
                    box.position = player.position - new Vector3(0, 1, 0);
                    heldBox = box;
                    heldBox.gravityScale = 0;
                    objectHeld = true;
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
                heldBox.transform.parent = null;
                heldBox.gravityScale = 1;
                objectHeld = false;
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
        //Check if it is a box
        if(other.CompareTag("LBox") || other.CompareTag("MBox") || other.CompareTag("HBox") && !objectHeld)
        {
            //See if the human can lift it
            if (player.CompareTag("Human") && (other.CompareTag("LBox") || other.CompareTag("MBox")) && (player.position.x < other.transform.position.x || player.position.x > other.transform.position.x)
                /*(player.position.y > other.transform.position.y - 0.25 && player.position.y < other.transform.position.y + 0.25)*/)
            { 
                box = other.attachedRigidbody;
                SelectBox(other.transform);
                canHold = true;
            }
            //See if the bat can lift it
            else if (player.CompareTag("Bat") && (other.CompareTag("LBox")) && player.position.y > other.transform.position.y + 0.5)
            {
                box = other.attachedRigidbody;
                SelectBox(other.transform);
                canHold = true;
            }
            //See if the human can push it
            else if(player.CompareTag("Human") && (other.CompareTag("LBox") || other.CompareTag("MBox") || other.CompareTag("HBox")))
            {
                SelectBox(other.transform);
                canHold = true;
            }
            //See if anyone can push it
            else if(other.CompareTag("LBox") || other.CompareTag("MBox"))
            {
                SelectBox(other.transform);
                canHold = true;
            }
        }
        //Check if it is a switch
        else if(other.CompareTag("Lever"))
        {
            lever = other.GetComponent<Switch>();
        }
        //Check if it is a cat climb wall
        else if(other.CompareTag("Climb"))
        {
            climb = true;
        }
        //Check if it is swingable object
        else if(other.CompareTag("Swing"))
        {
            inRange = true;
            lamp = other.gameObject;
            indicatorPrefabClone = Instantiate(indicatorPrefab, other.transform.position, Quaternion.identity);
        }
    }

    //Remove boxes from selection
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HBox") || other.CompareTag("MBox") || other.CompareTag("LBox"))
        {
            box = null;
            SelectBox(null);
            canHold = false;
        }
        else if(other.CompareTag("Lever"))
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
                inRange = false;
                lamp = null;
                //tendril.transform.LookAt(null);
            }
        }

        Destroy(indicatorPrefab);
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

    public void ShootTentril()
    {
        if (!spring.isActiveAndEnabled)
        {
            /*LeanTween.scaleZ(tendril, spring.distance, 2);
            LeanTween.moveLocalY(tendril, spring.distance / 2, 2);
            tendSeg.enabled = true;
            tendSeg.connectedAnchor = tendSeg.transform.InverseTransformPoint(lamp.transform.position);
            LeanTween.scale(tendril, Vector3.one, 1);*/
            spring.enabled = true;
            spring.connectedAnchor = lamp.transform.position;
            lineRender.SetPosition(1, lamp.transform.position);
            isAttached = true;

            //Cooldown
            cooldownTime = 0.3f;
            specialReady = false;

        }
        else
        {
            /*LeanTween.scaleZ(tendril, 1, 2);
            LeanTween.moveLocalY(tendril, 0.5f, 2);
            tendSeg.enabled = false;
            tendSeg.connectedAnchor = Vector2.zero;
            LeanTween.scale(tendril, Vector3.zero, 1);*/
            spring.enabled = false;
            spring.connectedAnchor = Vector2.zero;
            lineRender.SetPosition(1, Vector3.zero);
            isAttached = false;

            //Cooldown
            cooldownTime = 0.5f;
            specialReady = false;
        }
        //init Cooldown
        StartCoroutine("SpecialCoolDown");
    }

    void CreateIndicator(Vector3 pos)
    {
        if(indicatorPrefabClone == null)
        {

        }
    }
}
