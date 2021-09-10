using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    //Created by Benathen on 9/10/2021
    /**
     *  TODO:
     *  
     * 
     */

    //Private but Accessible
    [SerializeField]
    int cpNum; //Change this value to teleport player to respective checkpoint
    [SerializeField]
    CameraControl camCtrl;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cpNum != 0)
        {

        }
    }
}
