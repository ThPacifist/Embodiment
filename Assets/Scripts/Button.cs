using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using System;

public class Button : MonoBehaviour
{
    public bool type; //true is for light buttons, false is for heavy buttons
    [SerializeField]
    Transform button;

    public Vector3 restPos;

    bool isTouching = false;

    public static Action Activate = delegate { };
    /*Light Buttons:
     * - Can be pressed by any creature or object
     * - Will stay pressed once pressed
     */

    /*Heavy Button:
     * - Can only be pressed by light and heavy boxes, and the human skeleton
     * - Will not stay pressed if object is moved off of button
     */

    // Start is called before the first frame update
    void Start()
    {
        restPos = button.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (type)
        {

        }
        else
        {
            if(!isTouching)
            {
                this.gameObject.transform.position = restPos;
            }
        }

        if(this.gameObject.transform.position != restPos)
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
}