using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineTargetGroupCamSwitch : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera vCam;

    //Should only detect player because the trigger is on the player detector layer
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit: Collision is " + collision);
        vCam.Priority = 2;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        vCam.Priority = 0;
    }
}
