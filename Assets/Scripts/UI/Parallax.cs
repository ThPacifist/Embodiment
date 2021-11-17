using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startposX, startposY;
    public GameObject cam;
    public float parallaxEffect;
    public Vector2 limits;

    // Start is called before the first frame update
    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffect);
        transform.position = new Vector2(startposX + dist, startposY + distY);

        /*(if(temp > startposX + length)
        {
            startposX += length;
        }
        else if(temp < startposX - length)
        {
            startposX -= length;
        }*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, limits);
    }
}
