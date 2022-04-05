using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    private void Awake()
    {
        Collider2D collider = GetComponent<Collider2D>();
        transform.position = GameAction.PlaceColOnGround(collider);
    }

    public void DisableGameObject()
    {
        Destroy(gameObject);
    }
}
