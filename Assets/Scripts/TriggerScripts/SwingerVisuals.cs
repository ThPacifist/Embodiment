using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingerVisuals : MonoBehaviour
{
    /*
     * Handles Blobs swing mechanics
     * When the blob walks into its trigger, it grabs the special interactions script
     * Then passes this game object to the script using the built in function, which gives the player the object the player can attach too
     * Also activates the indicator when the player is within range
     */
    public GameObject indicator;
    [SerializeField]
    BlobController interaction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blob"))
        {
            interaction = collision.GetComponent<BlobController>();
            if (!interaction.isAttached)
            {
                if (interaction != null)
                    interaction.SetSwingerGameObject(gameObject);
                //indicator.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Blob"))
        {
            if (interaction != null)
            {
                if (interaction.lamp == this.gameObject)
                {
                    interaction.SetSwingerGameObject(null);
                    interaction = null;
                }
                //indicator.SetActive(false);
            }
        }
    }
}
