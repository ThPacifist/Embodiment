using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRespawn : MonoBehaviour
{
    //Public variables
    public Transform respawnPos;

    //When it enters any of these triggers, reapwawn the box
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other);
        if (other.tag == "Death")
        {
            this.transform.position = respawnPos.position;
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(respawnPos.position, 0.5f);
    }
}
