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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CenterCamera()
    {
        cam.transform.position = new Vector3(player.position.x, player.position.y, cam.transform.position.z);
    }
}
