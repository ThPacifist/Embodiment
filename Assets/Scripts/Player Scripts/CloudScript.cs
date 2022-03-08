using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    private void Awake()
    {
        Collider2D collider = GetComponent<Collider2D>();
        Debug.Log(collider);
        transform.position = GameAction.PlaceColOnGround(collider);
        Debug.Log("Awake is called");
    }

    public void DisableGameObject()
    {
        Destroy(gameObject);
    }
}
