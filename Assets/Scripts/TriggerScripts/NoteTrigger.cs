using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrigger : MonoBehaviour
{
    public Controller controller;
    public GameObject buttonPrompt;
    public GameObject noteObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        controller = other.GetComponent<Controller>();
        if(controller != null)
        {
            controller.note = noteObject;
            buttonPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (controller != null)
        {
            controller.note = null;
            buttonPrompt.SetActive(false);
        }
    }
}
