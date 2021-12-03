using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //Public variables
    public GameObject cam;
    public float parallaxEffect;
    public float limits;
    

    //Private variables
    private Vector2 origin;
    private Vector2 deviation;
    private Vector2 distance;

    // Start is called before the first frame update
    void Start()
    {
        //Set the middle position
        origin = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Find the direction of the camera from this object
        distance = cam.transform.position;
        distance = origin - distance;
        deviation = distance;
        deviation.x *= -1;
        deviation.Normalize();
        //Find the magnitude of the effect
        parallaxEffect = Mathf.Sqrt((distance.x * distance.x) + (distance.y * distance.y));
        parallaxEffect *= 0.1f;
        if(parallaxEffect > limits)
        {
            parallaxEffect = limits;
        }
        deviation *= parallaxEffect;
        //Place the object
        transform.position = origin + deviation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector2(limits,limits));
    }
}
