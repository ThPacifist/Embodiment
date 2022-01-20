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

    //Private variables
    private bool targetSpawned;

    //Serialize fields
    [SerializeField]
    SpecialInteractions interaction;
    [SerializeField]
    GameObject target;

    //When the player enters the trigger
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Get the special interactions from the player
        interaction = collision.GetComponent<SpecialInteractions>();

        //Spawn the interaction indicator
        if(interaction.prefabInstance == null)
        {
            interaction.prefabInstance = Instantiate(target, note.transform);
            targetSpawned = true;

            //Set notes
            interaction.note = note;
        }

        
    }

    //When the player leaves the trigger
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (targetSpawned)
        {
            //Destroy the target
            Destroy(interaction.prefabInstance);
            targetSpawned = false;

            //Unset note
            interaction.note = null;
        }


    }
}
