using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject eyeSprite;

    private void Update()
    {
        eyeSprite.transform.localPosition = new Vector3(
            Mathf.Clamp(rb.velocity.x * 0.3f, -0.2f, 0.2f), 
            Mathf.Clamp(rb.velocity.y * 0.3f, -0.14f, 0.14f), 
            0f);   
    }
}
