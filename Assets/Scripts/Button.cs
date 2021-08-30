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
                Debug.Log("Behind LeanTween");
                LeanTween.moveY(this.gameObject, restPos.y, 1);
            }
        }

        if(GetComponent<Collider>().bounds.max.y != restPos.y)
        {
            Activate();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        isTouching = true;
        Debug.Log("Is Touching");
    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(WaitForTouch());
    }

    IEnumerator WaitForTouch()
    {
        yield return new WaitForSeconds(4);
        isTouching = false;
        Debug.Log("Is not Touching");
    }
}