using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using System;

public class Switch : MonoBehaviour
{
    /*
     * Description:
     * Handles buttons, weighted buttons, weight plates, levers of all kinds
     * Plates cannot be light or a lever (should be heavy, but can sometimes be medium
     * It must be light, medium, heavy, or a lever no matter what
     * If you want it to be weighted set the reqWeight to the weight (1 is light, 2 is medium, 3 is heavy)
     */
    [SerializeField]
    Transform button;
    [SerializeField]
    GameObject buttonBase;
    [SerializeField]
    SpringJoint2D spring;

    [SerializeField]
    GameAction[] behaviors; //behavior that is triggered when the switch is active

    //Public Variables
    public bool Light;
    public bool Medium;
    public bool Heavy;
    public bool Lever;
    public bool weight;
    public bool plate;
    public int reqWeight;

    //Private Variables
    Vector3 restPos;
    public bool isTouching = false;
    public bool active;
    private int currentWeight = 0;
    bool b;
    public float dist;

    /* Light Buttons:
     * - Press the interact button to activate the button
     */

    /* Medium Buttons:
     * - Can be pressed by any creature or object
     * - Will stay pressed once pressed
     */

    /* Heavy Button:
     * - Can only be pressed by light and heavy boxes, and the human skeleton
     * - Will not stay pressed if object is moved off of button
     */

    /* Lever:
     * - Can be toggled on/off like a lever
     */

    /* Weight:
     * - Needs a certain weight to be on it before it is triggered
     * - Public variable weight needs to be set to 1, 2, or 3 for light boxes - heavy boxes
     * - Plate bool makes it need that specific weight, if more is added, it doesn't work
     */

    // Start is called before the first frame update
    void Start()
    {
        restPos = button.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the distance between the button and the base
        dist = Vector2.Distance(this.gameObject.transform.position, spring.transform.position);
        if (Heavy)
        {
            if(!isTouching || !active)// If there is not an object touching the button, move button to its original position
            {
                //Calls the behavior's action function with a bool parameter
                //For right now it calls the DisableSprites action function, which re-enables the sprite
                //It does this by using the passed in bool, to toggle the sprite
                ActivateBehavior(true);
            }
        }

        if(Heavy || Medium)// If either the heavy or medium bool is true
        {
            if(dist < 0.3f) //If the position of the button is less than its rest position, activate the behavior
            {
                if (weight)
                {
                    if(plate && currentWeight == reqWeight)
                    {
                        ActivateBehavior(false);
                        active = true;
                    }
                    else if(currentWeight >= reqWeight)
                    {
                        ActivateBehavior(false);
                        active = true;
                    }
                }
                else if(Medium)
                {
                    spring.autoConfigureConnectedAnchor = false;
                    ActivateBehavior(false);
                    active = true;
                }
                //Calls the behavior's action function with a bool parameter
                //For right now it calls the DisableSprites action function, which disables the sprite
                //It does this by using the passed in bool, to toggle the sprite
                else
                {
                    ActivateBehavior(false);
                    active = true;
                }
            }
        }
    }

    //Medium and heavy enter
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Check if the button is medium/heavy
        if (Medium || Heavy)
        {
            isTouching = true;
            //Get weight of object weighing the button down
            if (weight)
            {
                switch(other.gameObject.tag)
                {
                    case "LBox":
                        currentWeight = 1;
                        break;
                    case "MBox":
                        currentWeight = 2;
                        break;
                    case "HBox":
                        currentWeight = 3;
                        break;
                    default:
                        currentWeight = 0;
                        break;
                }
            }
        }
    }
    //Exit for medium and heavy
    private void OnCollisionExit2D(Collision2D other)
    {
        //Check if the button is medium/heavy
        if (Medium || Heavy)
        {
            //Not active, nothing is touching it, reset weight
            active = false;
            isTouching = false;
            if(weight && !GameAction.PlayerTags(other.gameObject.tag))
            {
                currentWeight = 0;
            }
        }
    }
    //When the player gets in range of a light button or a lever, it subscribes the interact function here to the interact action
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if it is a player that touched it
        if (GameAction.PlayerTags(other.tag))
        {
            //Check if it is light or a lever
            if (Light || Lever)
            {
                b = true;
                PlyController.Interact += Interact;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Check if it is a player that stopped touching it
        if (GameAction.PlayerTags(other.tag))
        {
            //Check if the button is light or a lever
            if (Light || Lever)
            {
                b = false;
                PlyController.Interact -= Interact;
            }
        }
    }

    public void Interact()
    {
        //This version of the behavior action call, calls the DisableSprite action function without the parameter
        //This intern calls a function which disables the sprite but without passing in a bool
        ActivateBehavior();
    }

    void ActivateBehavior()
    {
        foreach(GameAction behavior in behaviors)
        {
            behavior.Action();
        }
    }

    void ActivateBehavior(bool b)
    {
        foreach (GameAction behavior in behaviors)
        {
            behavior.Action(b);
        }
    }
}