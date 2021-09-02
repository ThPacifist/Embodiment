using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    [SerializeField]
    Transform player;

    public GameObject topRight;
    public GameObject botLeft;
    
    // Start is called before the first frame update
    void Start()
    {
        CenterCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckBotLeft() || CheckTopRight())
        {
            CenterCamera();
        }
    }

    void CenterCamera()
    {
        cam.transform.position = new Vector3(player.position.x, player.position.y, cam.transform.position.z);
    }

    bool CheckTopRight()
    {
        if(cam.ViewportToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, 0)).x > topRight.transform.position.x ||
           cam.ViewportToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, 0)).y > topRight.transform.position.y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool CheckBotLeft()
    {
        if(cam.ViewportToWorldPoint(Vector3.zero).x < botLeft.transform.position.x ||
           cam.ViewportToWorldPoint(Vector3.zero).y < botLeft.transform.position.y)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

    bool CheckCamera()
    {
        return true;
    }
}
