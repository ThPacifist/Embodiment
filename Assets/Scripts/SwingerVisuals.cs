using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingerVisuals : MonoBehaviour
{
    public GameObject indicator;
    public SpecialInteractions temp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        temp = collision.GetComponent<SpecialInteractions>() as SpecialInteractions;
        if (temp != null)
            temp.SetSwingerGameObject(gameObject);
        indicator.SetActive(true);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        temp = null;
        indicator.SetActive(false);
    }
}
