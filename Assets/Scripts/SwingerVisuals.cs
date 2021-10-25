using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingerVisuals : MonoBehaviour
{
    public GameObject indicator;
    [SerializeField]
    SpecialInteractions interaction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Blob"))
        {
            interaction = collision.GetComponent<SpecialInteractions>();
            if (interaction != null)
                interaction.SetSwingerGameObject(gameObject);
            indicator.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interaction = null;
        indicator.SetActive(false);
    }
}
