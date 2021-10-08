using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using System;

public class Switch : MonoBehaviour
{
    [SerializeField]
    Transform button;

    [SerializeField]
    GameAction behavior; //behavior that is triggered when the switch is active

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
        if (Heavy)
        {
            if(!isTouching)// If there is not an object touching the button, move button to its original position
            {
                resetButton();
                //Calls the behavior's action function with a bool parameter
                    //For right now it calls the DisableSprites action function, which re-enables the sprite
                        //It does this by using the passed in bool, to toggle the sprite
                behavior.Action(true);
            }
        }
        if(Heavy || Medium)// If either the heavy or medium bool is true
        {
            if(this.gameObject.transform.position.y < restPos.y) //If the position of the button is less than its rest position, activate the behavior
            {
                if (weight)
                {
                    if(plate && currentWeight == reqWeight)
                    {
                        behavior.Action(false);
                    }
                    else if(currentWeight >= reqWeight)
                    {
                        behavior.Action(false);
                    }
                }
                //Calls the behavior's action function with a bool parameter
                //For right now it calls the DisableSprites action function, which disables the sprite
                //It does this by using the passed in bool, to toggle the sprite
                else
                {
                    behavior.Action(false);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Medium || Heavy)
        {
            isTouching = true;
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
                        currentWeight = 1;
                        break;
                    default:
                        currentWeight = 0;
                        break;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (Medium || Heavy)
        {
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
        if (GameAction.PlayerTags(other.tag))
        {
            if (Light || Lever)
            {
                b = true;
                PlyController.Interact += Interact;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (GameAction.PlayerTags(other.tag))
        {
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
        behavior.Action();
    }

    //Returns the button to its starting position, which is its resting position
    void resetButton()
    {
        if(this.gameObject.transform.position.y < restPos.y)
        {
            this.gameObject.transform.position += new Vector3 (0, Time.deltaTime, 0) * 0.3f;
        }
    }
}