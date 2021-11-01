using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointReset : MonoBehaviour
{
    //Public variables and assets
    public Transform target;
    public bool activeAtStart;

    //Private variables
    private Vector2 resetPos;
    private Quaternion resetRot;

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        PlyController.Death += ResetPos
            ;
    }

    private void OnDisable()
    {
        PlyController.Death -= ResetPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        resetPos = target.position;
        resetRot = target.rotation;
    }

    
    //This function is called when the player dies and resets the objects position
    private void ResetPos()
    {
        target.position = resetPos;
        target.rotation = resetRot;
        if(!activeAtStart)
        {
            target.gameObject.SetActive(false);
        }
    }
}
