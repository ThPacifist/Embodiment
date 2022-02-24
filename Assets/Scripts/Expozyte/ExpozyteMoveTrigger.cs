using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpozyteMoveTrigger : MonoBehaviour
{
    /*
     * Description:
     * This trigger begins Expozyte's move to a new checkpoint
     */

    //Public Variables
    public ExpozyteMove ExpMove;
    public int goToCheckpoint;

    //When the player enters move
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Begin move
        ExpMove.BeginMove(goToCheckpoint);
        this.gameObject.SetActive(false);
    }

}
