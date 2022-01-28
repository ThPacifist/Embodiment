using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseData : MonoBehaviour
{
    /*
     * Description:
     * This is the parent class of the Data classes
     * It has a constructor to build data initially
     * It has RebuildData to rebuild on checkpoints
     * It has ResetData to reset data on death
     */

    //Assets
    public GameObject savedObject;

    //Enable on enable and disable on disable
    private void OnEnable()
    {
        InitializeData();
        CheckpointController.RebuildData += SaveState;
        TransitionController.slideOutAction += ResetData;
    }

    private void OnDisable()
    {
        CheckpointController.RebuildData -= SaveState;
        TransitionController.slideOutAction -= ResetData;
    }

    //Constructs the data at the beginning, called by awake
    public virtual void InitializeData()
    {
        savedObject = this.gameObject;
        Debug.Log("Initialized " + gameObject.name);
    }

    //Rebuilds data when checkpoint it hit
    public virtual void SaveState()
    {
        Debug.Log("Saved " + gameObject.name);
    }

    //Resets data when the player dies
    public virtual void ResetData()
    {
        Debug.Log("Reset: " + gameObject.name);
    }
}
