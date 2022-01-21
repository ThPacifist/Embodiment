using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNote : MonoBehaviour
{
    /*
     * Description:
     * This script detects when the player is near the note and allows it to show up when they press interact
     * This script also allows them to put away the graphic as well
     */

    //Public variables
    public GameObject note;
    public GameObject trigger;

    //Private variables
    private bool playerNear;
    private GameObject targetInstance;

    //Serialize fields
    [SerializeField]
    GameObject target;

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        PlyController.Interact += Interact;
    }

    private void OnDisable()
    {
        PlyController.Interact -= Interact;
    }

    //When the player enters the trigger
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Spawn the interaction indicator
        if (!playerNear)
        {
            targetInstance = Instantiate(target, trigger.transform);
            playerNear = true;
        }
    }

    //When the player leaves the trigger
    public void OnTriggerExit2D(Collider2D collision)
    {
        //Destroy the target
        if (playerNear)
        {
            Destroy(targetInstance);
            playerNear = false;

            //Turn off the note if it is on
            if(note.active)
            {
                note.SetActive(false);
            }
        }
    }

    //Interact with the note
    private void Interact()
    {
        //Check if there is a note to interact with
        if (playerNear)
        {
            if (!note.active)
            {
                note.SetActive(true);
            }
            else
            {
                note.SetActive(false);
            }
        }
    }
}
