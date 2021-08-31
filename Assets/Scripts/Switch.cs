using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using System;

public class Switch : MonoBehaviour
{
    [SerializeField]
    Transform button;

    public bool Light;
    public bool Medium;
    public bool Heavy;
    public bool Lever;

    Vector3 restPos;

    bool isTouching = false;

    public static Action Activate = delegate { };

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
                this.gameObject.transform.position = restPos;
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
            Activate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isTouching = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isTouching = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Light || Lever)
        {
            LandMovement.Interact += Interact;
            WaterMovement.Interact += Interact;
            AirMovement.Interact += Interact;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Light || Lever)
        {
            LandMovement.Interact -= Interact;
            WaterMovement.Interact -= Interact;
            AirMovement.Interact = Interact;
        }
    }

    private void Interact()
    {
        active = true;
    }
}