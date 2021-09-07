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
    public bool withinX = true;
    public bool withinY = true;

    Vector3 center;
    // Start is called before the first frame update
    void Start()
    {
        center = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0));
        CenterCamera();
    }

    // Update is called once per frame
    void Update()
    {
        center = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0));
        if (CheckPosX() || CheckPosY())
        {
            if(CheckPosX() && !CheckPosY()) // Cam excedes the x limit
            {
                CenterCamera("x");
            }
            if(!CheckPosX() && CheckPosY()) // Cam excedes the y limit
            {
                CenterCamera("y");
            }
            if(CheckPosX() && CheckPosY()) // Cam does not excedes any limit
            {
                CenterCamera();
            }
        }
    }

    void CenterCamera(string change = "all")
    {
        if (change == "all")
        {
            cam.transform.position = new Vector3(player.position.x, player.position.y, cam.transform.position.z);
        }
        else if (change == "x")
        {
            cam.transform.position = new Vector3(player.position.x, cam.transform.position.y, cam.transform.position.z);
        }
        else if (change == "y")
        {
            cam.transform.position = new Vector3(cam.transform.position.x, player.position.y, cam.transform.position.z);
        }
    }

    bool CheckPosX()
    {
        if(cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, 0)).x > topRight.transform.position.x)
        {
            ReAdjustX("+x");
            return false;
        }
        else if(cam.ScreenToWorldPoint(Vector3.zero).x < botLeft.transform.position.x)
        {
            ReAdjustX("-x");
            return false;
        }
        else
        {
            return true;
        }
    }

    bool CheckPosY()
    {
        if(cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, 0)).y > topRight.transform.position.y)
        {
            ReAdjustY("+y");
            return false;
        }
        else if (cam.ScreenToWorldPoint(Vector3.zero).y < botLeft.transform.position.y)
        {
            ReAdjustY("-y");
            return false;
        }
        else
        {
            return true;
        }
    }

    void ReAdjustX(string posX)
    {
        if(posX == "+x")
        {
            if(player.position.x < center.x)
            {
                CenterCamera("x");
            }
        }
        else if(posX == "-x")
        {
            if(player.position.x > center.x)
            {
                CenterCamera("x");
            }
        }
    }

    void ReAdjustY(string posY)
    {
        if(posY == "+y")
        {
            if(player.position.y < center.y)
            {
                CenterCamera("y");
            }
        }
        else if(posY == "-y")
        {
            if(player.position.y > center.y)
            {
                CenterCamera("y");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Top Right Lines
        Gizmos.DrawLine(topRight.transform.position, new Vector3(botLeft.transform.position.x, topRight.transform.position.y, 0));
        Gizmos.DrawLine(topRight.transform.position, new Vector3( topRight.transform.position.x, botLeft.transform.position.y, 0));
        //Bottom Left Lines
        Gizmos.DrawLine(botLeft.transform.position, new Vector3(botLeft.transform.position.x, topRight.transform.position.y, 0));
        Gizmos.DrawLine(botLeft.transform.position, new Vector3(topRight.transform.position.x, botLeft.transform.position.y, 0));
    }
}
