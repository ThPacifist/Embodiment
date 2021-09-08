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
    GameAction behavior; //behavior that is triggered when this switch is active

    //Public Variables
    public bool Light;
    public bool Medium;
    public bool Heavy;
    public bool Lever;

    //Private Variables
    public Vector3 restPos;
    bool isTouching = false;
    bool active;

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
            if(!isTouching)
            {
                resetButton();
                active = false;
            }
        }

        if(Heavy || Medium)
        {
            if(this.gameObject.transform.position != restPos)
            {
                active = true;
            }
        }

        if(active)
        {
            behavior.Action();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Medium || Heavy)
        {
            isTouching = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (Medium || Heavy)
        {
            isTouching = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Light || Lever)
        {
            LandMovement.Interact += Interact;
            AirMovement.Interact += Interact;
        }
        else if(Light)
        {
            WaterMovement.Interact += Interact;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (Light || Lever)
        {
            LandMovement.Interact -= Interact;
            AirMovement.Interact = Interact;
        }
        else if(Light)
        {
            WaterMovement.Interact -= Interact;
        }
    }

    public void Interact()
    {
        active = true;
    }

    void resetButton()
    {
        if(this.gameObject.transform.position.y > restPos.y)
        {
            this.gameObject.transform.position += new Vector3 (0, Time.deltaTime, 0);
        }
    }
}